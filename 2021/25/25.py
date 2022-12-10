
trench = [["." for row in range(0, 139)] for col in range(0,137)]
#trench = [["." for row in range(0, 10)] for col in range(0,9)]
movemenMap = [[False for row in range(0, 139)] for col in range(0,137)]
#movemenMap = [[False for row in range(0, 10)] for col in range(0,9)]

globalMovement = True

def inputToArray():
    with open("input.txt", "r") as f:
        lines = f.readlines()


    for i in range(0, len(lines), 1):
        for j in range(0, len(lines[0])-1, 1):
            trench[i][j] = lines[i][j:j+1:1]

def movementEastCheck():
    global globalMovement
    for x in range(0, len(trench), 1):
        for y in range(0, len(trench[0])-1, 1):
            if trench[x][y] == ">" and trench[x][y+1] ==".":
                movemenMap[x][y] = True
            else:
                movemenMap[x][y] = False
        # Check for last col if first col is free
        if trench[x][len(trench[0])-1] == ">" and trench[x][0] == ".":
            movemenMap[x][len(trench[0])-1] = True
        else:
            movemenMap[x][len(trench[0])-1] = False

    movementEast()

def movementEast():
    global globalMovement
    for x in range(0, len(trench), 1):
        for y in range(0, len(trench[0])-1, 1):
            if movemenMap[x][y] == True:
                trench[x][y] = "."
                trench[x][y+1] = ">"
                globalMovement = True
        if movemenMap[x][len(trench[0])-1] == True:
            trench[x][len(trench[0])-1] = "."
            trench[x][0] = ">"
            globalMovement = True

def movementSouthCheck():
    global globalMovement

    for x in range(0, len(trench)-1, 1):
        for y in range(0, len(trench[0]), 1):
            if trench[x][y] == "v" and trench[x+1][y] ==".":
                movemenMap[x][y] = True
            else:
                movemenMap[x][y] = False
    for y in range(0, len(trench[0]), 1):
        if trench[len(trench)-1][y] == "v" and trench[0][y] == ".":
            movemenMap[len(trench)-1][y] = True
        else:
            movemenMap[len(trench)-1][y] = False

    movementSouth()

def movementSouth():
    global globalMovement
    for x in range(0, len(trench)-1, 1):
        for y in range(0, len(trench[0]), 1):
            if movemenMap[x][y] == True:
                trench[x][y] = "."
                trench[x+1][y] = "v"
                globalMovement = True
    for y in range(0, len(trench[0]), 1):
        if movemenMap[len(trench)-1][y] == True:
            trench[len(trench)-1][y] = "."
            trench[0][y] = "v"
            globalMovement = True

def checkForChanges():
    counter = 0
    global globalMovement
    printArray()
    while globalMovement == True:
        globalMovement = False
        movementEastCheck()
        movementSouthCheck()
        counter = counter +1

        print()
        print("Step: " + str(counter))
        printArray()
    
    print(counter)

def printArray():
    for i in range(0, len(trench)):
        string = ""
        for j in range(0, len(trench[0]),1):
            string = string + trench[i][j]
        print(string)

inputToArray()
checkForChanges()