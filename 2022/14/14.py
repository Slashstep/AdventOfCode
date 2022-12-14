# Part 1: 1016, Part 2: 25402
x = 750
y = 250
cave = [["." for row in range(0, x)] for col in range(0,y)]

lines = []
sandStart = (500,0)
lowestPoint = 0

def inputToArray():
    global lines
    with open("input.txt", "r") as f:
        lines = f.readlines()

def visualizeCaveSystem(arr):
    # Pass Input into coordinates for stones    
    borderCoordinatesRaw = []
    for l in arr:
        borderCoordinatesRaw.append(l.split(sep=" -> "))

    borderCoordinates = []
    rock = []
    for b in borderCoordinatesRaw:
        for a in b:
            x, y = a.split(sep=",", maxsplit=1)
            if y[-1] == "\n":
                y = y[:-1]
            rock.append((int(x),int(y)))
        borderCoordinates.append(rock)
        rock = []

    # Go through coordinates and draw stones on grid
    for k in borderCoordinates:
        for l in range(0, len(k)-1, 1):
            #Get Change
            xChange = k[l+1][0] - k[l][0]
            yChange = k[l+1][1] - k[l][1]

            #Draw on grid
            if xChange > 0: #L2R
                for x in range(0, xChange, 1):
                    cave[k[l][1]][k[l][0]+x] = "#"
            elif xChange < 0: #R2L
                for x in range(xChange, 0, 1):
                    cave[k[l][1]][k[l][0]+x] = "#"
            elif yChange > 0:
                for y in range(0, yChange, 1):
                    cave[k[l][1]+y][k[l][0]] = "#"
            elif yChange < 0:
                for y in range(yChange, 0, 1):
                    cave[k[l][1]+y][k[l][0]] = "#"
            cave[k[l+1][1]][k[l+1][0]] = "#"

def findLowestPoint(cave):
    global lowestPoint
    for i, y in enumerate(cave):
        if "#" in y:
            lowestPoint = i + 2

    for i, x in enumerate(cave[lowestPoint]):
        cave[lowestPoint][i] = "#"

def dropSand(part):
    sandCorns = 0
    isTotalMovementPossible = True
    while isTotalMovementPossible == True:
        sandCorns += 1
        curCornX = sandStart[0]
        curCornY = sandStart[1]

        isCornMovable = True
        while isCornMovable == True:
            if cave[curCornY + 1][curCornX] == ".":
                cave[curCornY][curCornX] = "."
                cave[curCornY + 1][curCornX] = "+"
                curCornY += 1
            elif cave[curCornY + 1][curCornX-1] == ".":
                cave[curCornY][curCornX] = "."
                cave[curCornY + 1][curCornX-1] = "+"
                curCornY += 1
                curCornX -= 1
            elif cave[curCornY + 1][curCornX+1] == ".":
                cave[curCornY][curCornX] = "."
                cave[curCornY + 1][curCornX+1] = "+"
                curCornY += 1
                curCornX += 1
            else:
                cave[curCornY][curCornX] = "o"
                isCornMovable = False

            if part == 1:
                if curCornY == lowestPoint -1:
                    isTotalMovementPossible = False
                    sandCorns -= 1  #Cause this is the first Corn that touches the ground
                    break
            elif part == 2:
                if cave[sandStart[1]][sandStart[0]] == "o":
                    isTotalMovementPossible = False
                    break

    print(sandCorns)

def printArray(arr1):
    for i in range(0, len(arr1)):
        string = ""
        for j in range(0, len(arr1[0]),1):
            string = string + arr1[i][j]
        print(string)
    print()

inputToArray()
visualizeCaveSystem(lines)
findLowestPoint(cave)
dropSand(2)     # Enter 1 for Part 1 or 2 for Part 2
#printArray(cave)