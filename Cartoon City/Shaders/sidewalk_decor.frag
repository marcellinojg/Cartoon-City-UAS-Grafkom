#version 330

out vec4 outputColor;

//in vec4 vertexColor;

uniform vec4 ourColor;

void main()
{
	//outputColor = vertexColor;

	//outputColor = ourColor;

	outputColor = vec4(0.8,0.31, 0.4, 1.0);
	
}
