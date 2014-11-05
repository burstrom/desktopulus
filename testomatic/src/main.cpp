/*
	Testomatic.cpp
	Mockup for the vision of the project.
*/

#include "minko/Minko.hpp"
#include "minko/MinkoPNG.hpp"
#include "minko/MinkoSDL.hpp"

using namespace minko;
using namespace minko::component;
using namespace minko::math;

//Declaring texture files. Just for ease of use later.
const std::string TEXTURE1 = "1.png";
const std::string TEXTURE2 = "2.png";
const std::string TEXTURE3 = "3.png";

int main(int argc, char** argv)
{
	auto canvas = Canvas::create("Mockup", 1366, 768);

	auto sceneManager = SceneManager::create(canvas->context());
	
	// setup assets, you must queue all texture files here for them to be used.
	sceneManager->assets()->defaultOptions()->resizeSmoothly(true);
	sceneManager->assets()->defaultOptions()->generateMipmaps(true);
	sceneManager->assets()
		->registerParser<file::PNGParser>("png")
		->queue(TEXTURE1)
		->queue(TEXTURE2)
		->queue(TEXTURE3)
		->queue("effect/Basic.effect");

	sceneManager->assets()->geometry("cube", geometry::CubeGeometry::create(sceneManager->assets()->context()));

	//Create the root object, everything goes in here?
	auto root = scene::Node::create("root")
		->addComponent(sceneManager);

	//Create the "objects" we want to show in the scene, in this case the screens.
	//Translation tells us where it will be placed in the 3d space.
	//X, Y, Z, positive means right.
	auto screen1 = scene::Node::create("screen1")
		->addComponent(Transform::create(math::Matrix4x4::create()
			->translation(3.5f, 0.f, 0.5f)));


	auto screen2 = scene::Node::create("screen2")
		->addComponent(Transform::create(math::Matrix4x4::create()
			->translation(-3.5f, 0.f, 0.5f)));

	auto screen3 = scene::Node::create("screen3")
		->addComponent(Transform::create(math::Matrix4x4::create()
			->translation(0.f, 0.f, 0.f)));	

	auto invisActor = scene::Node::create("invisActor")
		->addComponent(Transform::create(math::Matrix4x4::create()
			->translation(0.f, 0.f, 35.f)));


	/*
		Add the camera, when doing the perspective, the first appears to be aspect ratio, the second appears to be fov?
	*/
	auto camera = scene::Node::create("camera")
		->addComponent(Renderer::create(0x7f7f7fff))
		->addComponent(Transform::create(
		Matrix4x4::create()->lookAt(Vector3::zero(), Vector3::create(0.f, 0.f, 3.f))
		))
		->addComponent(PerspectiveCamera::create(1366.f / 768.f, (float)PI * 0.01f, .1f, 1000.f));
		//->addComponent(PerspectiveCamera::create(1366.f / 768.f, (float)PI * 0.25f, .1f, 1000.f));

	/*
		Add the camera to the invisible actor
	*/	
	invisActor->addChild(camera);

	/*
		Add the invisible actor to the root.
	*/
	root->addChild(invisActor);

	/*
		In order to apply inertia to the camera.
	*/
	float rotSpeedX = 0.f; 
	float rotSpeedY = 0.f;

	/*
		Captures mouse movement to control camera.
	*/
	auto mouseMove = canvas->mouse()->move()->connect([&](input::Mouse::Ptr mouse, int dy, int dx){
		//Updates the rotation speed by checking the mouse movement and multiplying with a constant.
		rotSpeedX = (float)dx * .01f;
		rotSpeedY = (float)dy * .01f;

	});

	auto _ = sceneManager->assets()->complete()->connect([=](file::AssetLibrary::Ptr assets)
	{
		
		auto cubeGeometry = geometry::CubeGeometry::create(sceneManager->assets()->context());

		assets->geometry("cubeGeometry", cubeGeometry);

		//Adds texture mesh to the object.
		screen1->addComponent(Surface::create(
			assets->geometry("cubeGeometry"),
			material::BasicMaterial::create()->diffuseMap(assets->texture(TEXTURE1)),
			assets->effect("effect/Basic.effect")
			));
		//Defines initial rotation of object.
		screen1->component<Transform>()->matrix()->prependRotationY((float)PI*0.97f);

		screen2->addComponent(Surface::create(
			assets->geometry("cubeGeometry"),
			material::BasicMaterial::create()->diffuseMap(assets->texture(TEXTURE2)),
			assets->effect("effect/Basic.effect")
			));
		screen2->component<Transform>()->matrix()->prependRotationY(-(float)PI*0.97f);

		screen3->addComponent(Surface::create(
			assets->geometry("cubeGeometry"),
			material::BasicMaterial::create()->diffuseMap(assets->texture(TEXTURE3)),
			assets->effect("effect/Basic.effect")
			));

		//Adds objects to root.
		root->addChild(screen1);
		root->addChild(screen2);
		root->addChild(screen3);
	});

	auto resized = canvas->resized()->connect([&](AbstractCanvas::Ptr canvas, uint w, uint h)
	{
		camera->component<PerspectiveCamera>()->aspectRatio((float)w / (float)h);
	});

	auto enterFrame = canvas->enterFrame()->connect([&](Canvas::Ptr canvas, float time, float deltaTime)
	{
		//Updates the actual camera orientation.
		camera->component<Transform>()->matrix()->appendRotationY(rotSpeedY);
    	//camera->component<Transform>()->matrix()->appendRotationX(rotSpeedX);

    	//Scales the "after the fact"-movement to make sure the camera stops moving.
		rotSpeedX *= .10f;
		rotSpeedY *= .10f;

		//"advances" the scene.
		sceneManager->nextFrame(time, deltaTime);
	});

	sceneManager->assets()->load();
	canvas->run();

	return 0;
}


