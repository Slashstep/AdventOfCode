# Part 1: 1679, Part 2: 536625

forest = [["." for row in range(0, 99)] for col in range(0,99)]
scenicMap = [[0 for row in range(0, 99)] for col in range(0,99)]

def inputToArray():
    with open("input.txt", "r") as f:
        lines = f.readlines()

    for i in range(0, len(lines), 1):
        for j in range(0, len(lines[0])-1, 1):
            forest[i][j] = int(lines[i][j:j+1:1])

def countVisibleTrees():
    outerX = len(forest[0])*2
    outerY = len(forest)*2-4
    insideVis = 0

    isVisible = False
    for x in range(1, len(forest)-1, 1):
        for y in range(1, len(forest[0])-1, 1):
            isVisible = False
            #Check up
            for z in range(0, x, 1):
                if forest[z][y] < forest[x][y]:
                    isVisible = True
                else:
                    isVisible = False
                    break
            if isVisible == True:
                insideVis = insideVis + 1
                continue
            # Check down
            for z in range(x+1, len(forest), 1):
                if forest[z][y] < forest[x][y]:
                    isVisible = True
                else:
                    isVisible = False
                    break
            if isVisible == True:
                insideVis = insideVis + 1
                continue
            # Check left
            for z in range(0, y, 1):
                if forest[x][z] < forest[x][y]:
                    isVisible = True
                else:
                    isVisible = False
                    break
            if isVisible == True:
                insideVis = insideVis + 1
                continue
            # Check right
            for z in range(y+1, len(forest[0]), 1):
                if forest[x][z] < forest[x][y]:
                    isVisible = True
                else:
                    isVisible = False
                    break
            if isVisible == True:
                insideVis = insideVis + 1
                continue

    total = outerX + outerY + insideVis
    print(total)
    
def scenicScore():
    for x in range(1, len(forest)-1, 1):
        for y in range(1, len(forest[0])-1, 1):
            #Check up
            up = 0
            for z in range(x-1, -1, -1):
                if forest[z][y] < forest[x][y]:
                    up = up + 1
                else:
                    up = up + 1
                    break
            # Check down
            down = 0
            for z in range(x+1, len(forest), 1):
                if forest[z][y] < forest[x][y]:
                    down = down + 1 
                else:
                    down = down + 1
                    break
            # Check left
            left = 0
            for z in range(y-1, -1, -1):
                if forest[x][z] < forest[x][y]:
                    left = left + 1
                else:
                    left = left + 1
                    break
            # Check right
            right = 0
            for z in range(y+1, len(forest[0]), 1):
                if forest[x][z] < forest[x][y]:
                    right = right + 1
                else:
                    right = right + 1
                    break

            scenicMap[x][y] = up * down * left * right
    
def findBestTreeSpot():
    curMax = 0
    for i in range(0, len(scenicMap), 1):
        for j in range(0, len(scenicMap[0]),1):
            if scenicMap[i][j] > curMax:
                curMax = scenicMap[i][j]

    print(curMax)

inputToArray()
countVisibleTrees()
scenicScore()
findBestTreeSpot()