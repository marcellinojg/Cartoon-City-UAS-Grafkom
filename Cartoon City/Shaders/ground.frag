#version 330

out vec4 outputColor;

//in vec4 vertexColor;

uniform vec4 ourColor;

void main()
{
	//outputColor = vertexColor;

	//outputColor = ourColor;

	outputColor = vec4(0.2116,1.0, 0, 1.0);
	
}
