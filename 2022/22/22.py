# Part 1: 66292, Part 2: #### soll 127012
# Facing is 0 for right (>), 1 for down (v), 2 for left (<), and 3 for up (^). The final password is the sum of 1000 times the row, 4 times the column, and the facing.

# y = 73, x = 66
# Answer part 1: 66292
# y = 3, x = 127
# Answer part 2: 127012

route = ""
nav = []
x = 150
y = 200
map = [["*" for row in range(x)] for col in range(y)]

def CalcRoute():
    global route, nav
    with open('route.txt', 'r') as f:
        route = f.readline()

    steps = ""
    for l in range(0, len(route)-1, 1):
        if route[l:l+1:1] != "R" and route[l:l+1:1] != "L":
            steps += route[l:l+1:1]
        else:
            # 0 is R means clockwise, 1 is L means counter clockwise
            nav.append((int(steps), 0 if route[l:l+1:1] == "R" else 1))
            steps=""

def CreateMap():
    global map
    with open('input.txt', 'r') as f:
        lines = f.read().splitlines()

    for l in range(len(lines)):
        for k in range(len(lines[l])):
            if lines[l][k:k+1:1] != " ":
                map[l][k] = lines[l][k:k+1:1]

def StartPosition(arr):
    for i in range(len(arr[0])):
        if arr[0][i] == ".":
            return i

def FollowRoute():
    global map
    right = (1, 0)
    left = (-1, 0)
    up = (0, -1)
    down = (0, 1)
    dirs = [right, down, left, up]
    
    position =(StartPosition(map), 0)
    dirIndex = 0
    direction = dirs[dirIndex]

    for l in nav:
        # Create possible new coordinates
        newX = position[0]
        newY = position[1]

        # For amount of steps do loop
        for s in range(l[0]):
            # Create possible new coordinates
            newX = (newX + direction[0]) % len(map[0])
            newY = (newY + direction[1]) % len(map)

            # If # stop
            if map[newY][newX] == "#":
                break
            # if * start loop through walls to find potential new position
            elif map[newY][newX] == "*":
                wNewX = newX
                wNewY = newY
                while map[wNewY][wNewX] == "*":
                    wNewX = (wNewX + direction[0]) % len(map[0])
                    wNewY = (wNewY + direction[1]) % len(map)
                
                # if wall directly at boarder reset position
                if map[wNewY][wNewX] == "#":
                    newX = (newX - direction[0]) % len(map[0])
                    newY = (newY - direction[1]) % len(map)
                # if no wall directly at boarder continue
                else:
                    newX = wNewX
                    newY = wNewY

            # Set positiom
            position = (newX, newY)
        # Turn Head
        dirIndex += 1 if l[1] == 0 else -1
        direction = dirs[dirIndex%len(dirs)]


    print(position)
    print(1000 * (position[1] + 1) + 4 * (position[0] + 1) + (dirIndex - 1)% len(dirs))

def CheckFaces(position):
    if position[0] >= 50 and position[0] < 100 and position[1] >= 0 and position[1] < 50:
        return 1
    elif position[0] >= 100 and position[0] < 150 and position[1] >= 0 and position[1] < 50:
        return 2
    elif position[0] >= 50 and position[0] < 100 and position[1] >= 50 and position[1] < 100:
        return 3
    elif position[0] >= 0 and position[0] < 50 and position[1] >= 100 and position[1] < 150:
        return 4
    elif position[0] >= 50 and position[0] < 100 and position[1] >= 100 and position[1] < 150:
        return 5
    elif position[0] >= 0 and position[0] < 50 and position[1] >= 150 and position[1] < 200:
        return 6


def FollowCube():
    global map
    right = (1, 0)
    left = (-1, 0)
    up = (0, -1)
    down = (0, 1)
    dirs = [right, down, left, up]

    faces = [1, 2, 3, 4, 5, 6]
    
    position =(StartPosition(map), 0)
    curFace = CheckFaces(position)
    dirIndex = 0
    direction = dirs[dirIndex]

    for l in nav:
        # Create possible new coordinates
        newX = position[0]
        newY = position[1]

        # For amount of steps do loop
        for s in range(l[0]):
            # Create possible new coordinates
            newX = (newX + direction[0]) % len(map[0])
            newY = (newY + direction[1]) % len(map)
            newFace = CheckFaces((newX, newY))

            # If # stop
            if map[newY][newX] == "#":
                break
            # Face Changed TODO
            if curFace != newFace:
                print()
            if map[newY][newX] == "*":
                wNewX = newX
                wNewY = newY
                while map[wNewY][wNewX] == "*":
                    wNewX = (wNewX + direction[0]) % len(map[0])
                    wNewY = (wNewY + direction[1]) % len(map)
                
                # if wall directly at boarder reset position
                if map[wNewY][wNewX] == "#":
                    newX = (newX - direction[0]) % len(map[0])
                    newY = (newY - direction[1]) % len(map)
                # if no wall directly at boarder continue
                else:
                    newX = wNewX
                    newY = wNewY

            # Set positiom
            position = (newX, newY)
        # Turn Head
        dirIndex += 1 if l[1] == 0 else -1
        direction = dirs[dirIndex%len(dirs)]


    print(position)
    print(1000 * (position[1] + 1) + 4 * (position[0] + 1) + (dirIndex - 1)% len(dirs))


def PrintArray(arr):
    string = ""
    for l in arr:
        for i in l:
            string += i

        print(string)
        string = ""

CalcRoute()
CreateMap()
FollowRoute()
