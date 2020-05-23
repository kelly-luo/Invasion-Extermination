#include <stdio.h>
#include <stdlih.h>
#include <time.h>


char getRandomNumber(void);
char getInput(void);
int checkSmallAlpha(char);

int main(void)
{
    char randAl = 0;
    char input = 0;
    int rightAnswer = 0;
    randAl = getRandomNumber();

    while(rightAnswer != 1)
    {
        input = getInput();

        input
    }


}

char getRandomNumber(void)
{
    char randAlpha = 0;
    srand(time(0));

    randAlpha = (rand() % 122) + 97;


    return randAlpha;

}

char getInput(void)
{
    char input;
    scanf(%c,&input)

    return input;
}

int checkSmallAlpha(char input)
{
    if(input < 97)
        input = input - 22;



}
