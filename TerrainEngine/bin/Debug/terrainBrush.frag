#version 420 core

in vec3 colour;
in vec2 pass_textureCoordinates;

vec3 position;

out vec4 out_Color;

uniform sampler2D modelTexture;

void main(void){
	position = vec3(0.5f,0,0.5f);

	vec4 textureColor = texture(modelTexture, pass_textureCoordinates);
	//if(textureColor.a < 0.5f){
	//	discard;
	//}
	//out_Color = textureColor;

	//if(pass_textureCoordinates.x > position.x && pass_textureCoordinates.y > position.z){
	//	out_Color = vec4(1,1,1,1);
	//}
	//else{
	//	out_Color = vec4(0,0,0,0);
	//}
	vec4 circleColor = vec4(0.9f,0.9f,0.9f,0.5f);
	if ((pass_textureCoordinates.x - position.x) * (pass_textureCoordinates.x - position.x) + 
		(pass_textureCoordinates.y - position.z) * (pass_textureCoordinates.y - position.z) < 0.2f){
		circleColor = vec4(0.9f,0.9f,0.9f,0f);
		
	}
	else{
		circleColor = vec4(0.1f,0.1f,0.1f,1f);
	}
	if (circleColor.a < 0.5f){
			discard;
		}
		out_Color = circleColor;
	
}

bool inCircle(float cx, float cz, float x, float z, float radius)
{
	return (x - cx) * (x - cx) + (z - cz) * (z - cz) < radius * radius;
}