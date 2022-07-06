#version 330

out vec4 outputColor;

//in vec4 vertexColor;

uniform vec4 ourColor;

void main()
{
	//outputColor = vertexColor;

	//outputColor = ourColor;

	outputColor = vec4(1.0,0.6, 0.5, 1.0);
//	outputColor = vec4(1.0,0.6, 0.5, 1.0);
	
}
