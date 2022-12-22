# Part 1: 3530, Part 2: 2000

lines = []
coordinates = []
droplet = [[[0 for depth in range(25)] for width in range(25)] for hight in range(25)]

def InputToArray():
    global lines
    with open("input.txt", "r") as f:
        lines = f.readlines()

def CreateDroplet():
    global coordinates
    for l in lines:
        x, y, z = l.split(sep=",", maxsplit=2)
        coordinates.append((int(x)+1, int(y)+1, int(z)+1))
        
        droplet[int(x)+1][int(y)+1][int(z)+1] = 1

def FindTotalSurface(num2Check):
    sCounter = 0
    for c in coordinates:
        #check x
        sCounter += 1 if c[0] + 1 < 25 and droplet[c[0]+1][c[1]][c[2]] == num2Check else 0
        sCounter += 1 if c[0] - 1 >= 0 and droplet[c[0]-1][c[1]][c[2]] == num2Check else 0

        #check y
        sCounter += 1 if c[1] + 1 < 25 and droplet[c[0]][c[1]+1][c[2]] == num2Check else 0
        sCounter += 1 if c[1] - 1 >= 0 and droplet[c[0]][c[1]-1][c[2]] == num2Check else 0

        #check z
        sCounter += 1 if c[2] + 1 < 25 and droplet[c[0]][c[1]][c[2]+1] == num2Check else 0
        sCounter += 1 if c[2] - 1 >= 0 and droplet[c[0]][c[1]][c[2]-1] == num2Check else 0

    print(sCounter)

def FindOutsideSurface():
    isTotalMovementPossible = True
    while isTotalMovementPossible == True:
        isWaterPossible = True
        x = 0
        y = 0
        z = 0
        while isWaterPossible == True:
            if x+1 < 25 and droplet[x+1][y][z] == 0:
                x += 1
            elif y+1 < 25 and droplet[x][y+1][z] == 0:
                y += 1
            elif z+1 < 25 and droplet[x][y][z+1] == 0:
                z += 1
            else:
                droplet[x][y][z] = 2
                isWaterPossible = False
                if x == 0 and y == 0 and z == 0:
                    isTotalMovementPossible = False

    hasOutsideZeros = True
    while hasOutsideZeros == True:
        hasOutsideZeros = False
        for i in range(len(droplet)):
            for j in range(len(droplet[i])):
                for k in range(len(droplet[i][j])):
                    if droplet[i][j][k] == 0:
                        #Check neighbors for 2
                            if droplet[i+1][j][k] == 2 or droplet[i-1][j][k] == 2 or droplet[i][j+1][k] == 2 or droplet[i][j-1][k] == 2 or droplet[i][j][k+1] == 2 or droplet[i][j][k-1] == 2:
                                droplet[i][j][k] = 2
                                hasOutsideZeros = True

    FindTotalSurface(2)

InputToArray()
CreateDroplet()
FindTotalSurface(0)
FindOutsideSurface()