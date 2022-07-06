#version 330

out vec4 outputColor;

//in vec4 vertexColor;

uniform vec4 ourColor;

void main()
{
	//outputColor = vertexColor;

	//outputColor = ourColor;

	outputColor = vec4(0.506,0.330,0.220, 1.0);
	
}
