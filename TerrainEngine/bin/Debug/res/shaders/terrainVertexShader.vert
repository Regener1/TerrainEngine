#version 420 core

in vec3 position;
in vec2 textureCoordinates;

out vec3 colour;
out vec2 pass_textureCoordinates;

out vec3 pass_brushPosition;
out float pass_brushRadius;

uniform mat4 transformationMatrix;
uniform mat4 projectionMatrix;
uniform mat4 viewMatrix;

uniform vec3 brushPosition;
uniform float brushRadius;
uniform float terrainSize;

void main(void){

	gl_Position = projectionMatrix * viewMatrix * transformationMatrix * vec4(position,1.0);
	
	pass_textureCoordinates = textureCoordinates * terrainSize;
	colour = vec3(position.x + 0.5, 0.0, position.y + 0.5);
	pass_brushPosition = brushPosition;
	pass_brushRadius = brushRadius;
}