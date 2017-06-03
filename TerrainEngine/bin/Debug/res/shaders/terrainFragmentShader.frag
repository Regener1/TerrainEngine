#version 420 core

in vec3 colour;
in vec2 pass_textureCoordinates;

in vec3 pass_brushPosition;
in float pass_brushRadius;
in float pass_terrainSize;

out vec4 out_Color;

uniform sampler2D backgroundTexture;
uniform sampler2D rTexture;
uniform sampler2D gTexture;
uniform sampler2D bTexture;
uniform sampler2D blendMap;

void main(void){

	vec4 blendMapColor = texture(blendMap, pass_textureCoordinates);

	float backTextureAmount = 1 - (blendMapColor.r + blendMapColor.g + blendMapColor.b);
	vec2 tiledCoords = pass_textureCoordinates * pass_terrainSize;

	vec4 circleColor = texture(modelTexture, pass_textureCoordinates);
	if ((pass_textureCoordinates.x - pass_brushPosition.x) * (pass_textureCoordinates.x - pass_brushPosition.x) + 
		(pass_textureCoordinates.y - pass_brushPosition.z) * (pass_textureCoordinates.y - pass_brushPosition.z) < pass_brushRadius* pass_brushRadius){
		
		circleColor = vec4(0.9f,0.9f,0.9f,1f);
		
	}

	out_Color = circleColor;

}
