#version 420 core

in vec3 position;

out vec3 colour;

uniform mat4 transformationMatrix;
uniform mat4 projectionMatrix;
uniform mat4 viewMatrix;

void main(void){

	gl_Position = projectionMatrix * viewMatrix * transformationMatrix * vec4(position,1.0);
	colour = vec3(1.0, 0.941, 0.961);
}