# Part 1: 3530, Part 2: ####

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

def FindTotalSurface():
    sCounter = 0
    for c in coordinates:
        #check x
        sCounter += 1 if c[0] + 1 < 25 and droplet[c[0]+1][c[1]][c[2]] == 0 else 0
        sCounter += 1 if c[0] - 1 >= 0 and droplet[c[0]-1][c[1]][c[2]] == 0 else 0

        #check y
        sCounter += 1 if c[1] + 1 < 25 and droplet[c[0]][c[1]+1][c[2]] == 0 else 0
        sCounter += 1 if c[1] - 1 >= 0 and droplet[c[0]][c[1]-1][c[2]] == 0 else 0

        #check z
        sCounter += 1 if c[2] + 1 < 25 and droplet[c[0]][c[1]][c[2]+1] == 0 else 0
        sCounter += 1 if c[2] - 1 >= 0 and droplet[c[0]][c[1]][c[2]-1] == 0 else 0

    print(sCounter)

def FindOutsideSurface():
    print()

InputToArray()
CreateDroplet()
FindTotalSurface()