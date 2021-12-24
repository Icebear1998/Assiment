#version 330 core

struct Material
{
	vec3 ambient;
	vec3 diffuse;
	vec3 specular;
	float shiness;
};
uniform Material material;
struct Light
{
    vec3 position;
	vec3 ambient;
	vec3 diffuse;
	vec3 specular;
};
uniform Light light;
uniform vec3 cameraPos;

in vec3 normal;
in vec3 fragPos;
in vec4 myColor;
in vec2 TexCoords;
out vec4 fragColor; 

uniform vec4 u_Color;
uniform sampler2D texture_diffuse1;

void main()
{ 
    vec3 lightDirection = normalize(light.position - fragPos);
    vec3 viewDirection = normalize(cameraPos - fragPos);
    vec3 reflect = reflect(-lightDirection, normal);

    vec3 ambient = light.ambient;//*material.ambient;

    float diff = max(0.0,dot(lightDirection, normal));
    vec3 diffuse = diff * light.diffuse*material.diffuse;

    float spec = pow(max(0.0,dot(reflect, viewDirection)), material.shiness);
    vec3 specular = spec * light.specular*material.specular;

    vec3 color = ambient + diffuse + specular;
    
    fragColor = texture(texture_diffuse1, TexCoords)*vec4(color, 1.0f); 
};