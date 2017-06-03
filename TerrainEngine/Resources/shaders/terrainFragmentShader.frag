#version 420 core

in vec2 pass_textureCoordinates;
in vec3 surfaceNormal;
in vec3 toLightVector;

out vec4 out_Color;


uniform sampler2D backgroundTexture;	//GL_TEXTURE0
uniform sampler2D rTexture;				//GL_TEXTURE1
uniform sampler2D gTexture;				//GL_TEXTURE2
uniform sampler2D bTexture;				//GL_TEXTURE3

uniform sampler2D blendMap;				//GL_TEXTURE4

uniform float terrainSize;
uniform vec3 lightColor;

uniform vec3 brushPosition;
uniform float brushRadius;

bool in_circle(float cx, float cz, float x, float z, float radius)
{
	return (x - cx) * (x - cx) + (z - cz) * (z - cz) < radius * radius;
}

void main(void){

	vec4 blendMapColor  = texture(blendMap, pass_textureCoordinates);

	float backTextureAmount = 1 - (blendMapColor.r + blendMapColor.g + blendMapColor.b);
	vec2 tiledCoords = pass_textureCoordinates * terrainSize;
	vec4 backgroundTextureColor = texture(backgroundTexture, tiledCoords) * backTextureAmount;	
	vec4 rTextureColor = texture(rTexture, tiledCoords) * blendMapColor.r;					
	vec4 gTextureColor = texture(gTexture, tiledCoords) * blendMapColor.g;				
	vec4 bTextureColor = texture(bTexture, tiledCoords) * blendMapColor.b;
	
	vec4 totalColor = backgroundTextureColor + rTextureColor + gTextureColor + bTextureColor;


	vec3 unitNormal = normalize(surfaceNormal);
	vec3 unitLightVector = normalize(toLightVector);
	float nDotl = dot(unitNormal, unitLightVector);
	float brightness = max(nDotl, 0.0);
	vec3 diffuse = brightness * lightColor;

	vec4 circleColor = vec4(diffuse, 1.0) * totalColor;

	if(in_circle(brushPosition.x, brushPosition.z, tiledCoords.x, tiledCoords.y, brushRadius)){
		circleColor = vec4(0.88f,0.88f,0.92f,0.5f);
	}

	out_Color = circleColor;

}
