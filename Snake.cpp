//Andre Sacilotto - 231396

#include <stdlib.h>
#include <GL/glut.h>

class Color {
public:
	float r;
	float g;
	float b;
	Color(float _r, float _g, float _b) {
		r = _r / 256;
		g = _g / 256;
		b = _b / 256;
	}
};

const int windowWidth = 500;
const int windowHeight = 500;

GLenum drawMode = GL_FILL;

Color lineColor = Color(0,144,49);

void init(void) {
	glClearColor(0, 0, 0, 1);
	glOrtho(0, windowWidth, 0, windowHeight, -1, 1);
}

void display(void)
{
	glClear(GL_COLOR_BUFFER_BIT);
	glPushMatrix();
	glTranslatef(100, 100, 0);
	glScalef(25, 25, 0);

	glColor3f(lineColor.r, lineColor.g, lineColor.b);
	glPolygonMode(GL_FRONT, drawMode);
	//glFrontFace(GL_CW) - Change to clockwise


	glBegin(GL_TRIANGLE_STRIP);
	// -- lower --
	//tail
	glVertex2f(-1, 3);
	glVertex2f(0, 1);
	glVertex2f(1.5, 1.2);

	//body
	glVertex2f(3, 0);
	glVertex2f(3, 1);

	glVertex2f(8, 0);
	glVertex2f(8, 1);

	glVertex2f(9.5, 1);

	// middle 
	glVertex2f(1, 6);

	glVertex2f(2.5, 6);
	glVertex2f(3, 7.5);

	glVertex2f(5, 6);
	glVertex2f(5, 7.5);

	//head
	//left
	glVertex2f(5.5, 7); //1, 2
	//top
	glVertex2f(6.5, 6.75); //3, 3
	glVertex2f(6, 6.75); //2, 2
	//right
	glVertex2f(6.5, 6); //3, 0
	glVertex2f(6, 6.5); //2, 1 
	//bot
	glVertex2f(5, 6); //0, 0
	glVertex2f(5.5, 6.5); //1, 1
	//left
	glVertex2f(5.5, 7); // 1,2

	glEnd();

	glPopMatrix();
	glFlush();
}

void keyboard(unsigned char key, int x, int y) 
{
	switch (key) {
		case ' ':
			// TROCA CAMUFLAGEM
			lineColor.r = ((float)rand() / (RAND_MAX));
			lineColor.g = ((float)rand() / (RAND_MAX));
			lineColor.b = ((float)rand() / (RAND_MAX));
			glutPostRedisplay();
		break;
		case 27:
			exit(0);
		break;
	}
}

void mouse(int button, int state, int x, int y)
{
	if (button == GLUT_LEFT_BUTTON && state == GLUT_DOWN)
		drawMode = drawMode == GL_FILL ? GL_LINE : GL_FILL;
}

int main(int argc, char** argv) {
	glutInit(&argc, argv);
	glutInitDisplayMode(GLUT_SINGLE | GLUT_RGB);
	glutInitWindowSize(windowWidth, windowHeight);
	glutInitWindowPosition(glutGet(GLUT_SCREEN_WIDTH) * .3, glutGet(GLUT_SCREEN_HEIGHT) * .2);
	glutCreateWindow("Desenha uma cobra");
	init();
	glutKeyboardFunc(keyboard);
	glutMouseFunc(mouse);
	glutDisplayFunc(&display);
	glutMainLoop();
	return 0;
}