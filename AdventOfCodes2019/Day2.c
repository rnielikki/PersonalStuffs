#include <stdio.h>
//very lazy to read file
int inputs[] = { 1,0,0,3,1,1,2,3,1,3,4,3,1,5,0,3,2,1,6,19,2,19,6,23,1,23,5,27,1,9,27,31,1,31,10,35,2,35,9,39,1,5,39,43,2,43,9,47,1,5,47,51,2,51,13,55,1,55,10,59,1,59,10,63,2,9,63,67,1,67,5,71,2,13,71,75,1,75,10,79,1,79,6,83,2,13,83,87,1,87,6,91,1,6,91,95,1,10,95,99,2,99,6,103,1,103,5,107,2,6,107,111,1,10,111,115,1,115,5,119,2,6,119,123,1,123,5,127,2,127,6,131,1,131,5,135,1,2,135,139,1,139,13,0,99,2,0,14,0 };

int calculate(int ops, int index1, int index2)
{
	switch (ops) {
	case 1:
		return *(inputs + index1) + *(inputs + index2);
	case 2:
		return *(inputs + index1) * (*(inputs + index2));
	default:
		printf("error");
		exit(-1);
	}
}
void one() {
	//"replace them" Damnit.
	inputs[1] = 12;
	inputs[2] = 2;
	int index = 0;
	while (*(inputs+index) != 99) {
		*(inputs+*(inputs+index+3)) = calculate(*(inputs+index), *(inputs+index + 1), *(inputs+index + 2));
		index += 4;
	}
	printf("%d\n", inputs[0]);

}
void main() {
	//Damnit don't understand what the part two means
	one();
}
/*
var inputs = [1,9,10,3,2,3,11,0,99,30,40,50];
function calculate(ops, index1, index2)
{
	switch (ops) {
		case 1:
			return inputs[index1] + inputs[index2];
		case 2:
			return inputs[index1] * inputs[index2];
		default:
			console.log("error");
	}
}
function main() {
	//"replace them" Damnit.
	inputs[1] = 12;
	inputs[2] = 2;
	var size = inputs.length;
	for (let i = 0; i < size; i+=4) {
		if (inputs[i] == 99) {
			break;
		}
		inputs[inputs[i + 3]] = calculate(inputs[i],inputs[i+1],inputs[i+2]);
	}
	console.log(inputs)
}
*/