#ifdef VERTEX_SHADER

#ifdef GL_ES
	precision mediump float;
#endif

#pragma include("Skinning.function.glsl")

attribute vec3 position;
attribute vec2 uv;

uniform mat4 modelToWorldMatrix;
uniform mat4 worldToScreenMatrix;

varying vec2 vertexUV;
varying vec3 vertexUVW;

void main(void)
{
	#ifdef DIFFUSE_MAP
		vertexUV = uv;
	#endif

	#ifdef DIFFUSE_CUBEMAP
		vertexUVW = position;
	#endif 

	vec4 pos = vec4(position, 1.0);

	#ifdef NUM_BONES
		pos = skinning_moveVertex(pos);
	#endif // NUM_BONES
	
	#ifdef MODEL_TO_WORLD
		pos = modelToWorldMatrix * pos;
	#endif
	
	gl_Position =  worldToScreenMatrix * pos;
}

#endif // VERTEX_SHADER
