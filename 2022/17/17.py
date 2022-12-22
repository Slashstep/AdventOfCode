# Part 1: 3181, Part 2: 1.570.434.782.634

jets = ""
arrSize = 10000
cave = [[" " for row in range(0, 7)] for col in range(0, arrSize)]
limit = 1000000000000   #Change for Part 1 or 2
stones = [[(0, 0), (1, 0), (2, 0), (3, 0)], [(1, 0), (0, 1), (1, 1), (2, 1), (1, 2)], [(2, 0), (2, 1), (0, 2), (1, 2), (2, 2)], [(0, 0), (0, 1), (0, 2), (0, 3)], [(0, 0), (1, 0), (0, 1), (1, 1)]]
stoneDic = {}
isCycleDetected = False

rockCounter = 0
cHeight = 0

currentStoneHeight = 0
jetCounter = 0

def inputToArray():
    global jets
    with open("input.txt", "r") as f:
        jets = f.readline()

def ArrayHeight(height):
    return arrSize - 1 - height

def rollingStones():
    global rockCounter
    while rockCounter < limit:
        form = rockCounter % 5
        match form:
            case 0:
                height = 0
            case 1:
                height = 2
            case 2:
                height = 2
            case 3:
                height = 3
            case 4:
                height = 1
        fallingStone(ArrayHeight(currentStoneHeight) - 3, form, height)
        rockCounter += 1

    print(cHeight + currentStoneHeight)    

def fallingStone(start, form, height):
    global jetCounter, currentStoneHeight, isCycleDetected, limit, cHeight, rockCounter
    direction = 0
    # Stone appears
    stone = [2, start - height]

    isDownPossible = True
    while isDownPossible == True:
        # Jet Push
        jC = jetCounter % len(jets)

        if jets[jC:jC+1:1] == "<":
            direction = -1
        else:
            direction = 1

        isLeftRightPossible = True
        for i in range(0, len(stones[form]), 1):
            newX = stone[0] + stones[form][i][0] + direction
            if newX < 0 or newX > 6 or cave[stone[1] + stones[form][i][1]][newX] != " ":
                isLeftRightPossible = False
                break

        if isLeftRightPossible == True:
            stone[0] += direction
        jetCounter += 1

        # Fall Down
        for i in range(0, len(stones[form]), 1):
            newY = stone[1] + stones[form][i][1] + 1
            if newY > len(cave) - 1 or cave[newY][stone[0] + stones[form][i][0]] != " ":
                isDownPossible = False
                break

        if isDownPossible == True:
            stone[1] += 1

    for i in stones[form]:
        cave[i[1] + stone[1]][i[0] + stone[0]] = "#"
    
    currentStoneHeight = findCurrentStoneHeight(stone[1])

    if isCycleDetected == False:
    
        yTupleMax = []
        for i in range(7):
            for j in range(ArrayHeight(currentStoneHeight) - 100, len(cave), 1):
                if cave[j][i] == "#" or j == len(cave)-1:
                    yTupleMax.append(j +1)
                    break

        minY = min(yTupleMax)
        yTuple = [mc - minY for mc in yTupleMax]
        yTuple.extend([form, jC])
        state = tuple(yTuple)

        # Cycle Detected
        if state in stoneDic:
            cycleLength = rockCounter - stoneDic[state]["stones"]
            cycleHeight = currentStoneHeight - stoneDic[state]["height"]

            remainingStones = limit - rockCounter

            rep = remainingStones // cycleLength
            rem = remainingStones % cycleLength

            cHeight = cycleHeight * rep
            rockCounter = limit - rem

            isCycleDetected = True
        else:
            stoneDic[state] = {"height": currentStoneHeight, "stones": rockCounter}

def findCurrentStoneHeight(stone):
    for i in range(ArrayHeight(currentStoneHeight) - 50, len(cave), 1):
        hasHashtag = False
        for j in range(7):
            if cave[i][j] == "#":
                hasHashtag = True
                break
        
        if hasHashtag == True:
            return arrSize - i

inputToArray()
rollingStones()
