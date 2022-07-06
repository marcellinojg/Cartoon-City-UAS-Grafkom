#version 330 core

layout (location = 0) in vec3 aPosition;
layout (location = 1) in vec3 aNormal;

//layout (location = 1) in vec3 aColor;
uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

out vec3 Normal;
out vec3 FragPos;

//out vec4 vertexColor;

void main(void) {
	gl_Position = vec4(aPosition, 1.0) * model * view * projection;
	FragPos = vec3(vec4(aPosition, 1.0) * model);
	Normal = aNormal * mat3(transpose(inverse(model)));


//	vertexColor = vec4(aColor, 1.0);
//	vertexColor = vec4(1.0,0.0,0.0,1.0);
//	vertexColor = vec4(1.0, 0.0, 0.0, 1.0);
}