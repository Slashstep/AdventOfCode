#Part 1: 6470, Part 2: 2658
#Bridge Array ist nicht notwenig, da lediglich die Koordinaten von H benötigt werden
tail = [["." for row in range(0, 300)] for col in range(0,400)]

curX = startX = 100 #Spaltennummer
curY = startY = 100 #Zeilennummer

tailX = [curX for row in range(0,9)]
tailY = [curY for row in range(0,9)]
#num = ["#"] #Part 1
num = ["1", "2", "3", "4", "5", "6", "7", "8", "9"] #Part 2

lines = []

def inputToArray():
    global lines
    with open("input.txt", "r") as f:
        lines = f.readlines()

def moveHead():
    global curX, curY
    for l in lines:
        direction, steps = l.split()
        #print(direction + ", " + steps)
        for i in range(0, int(steps), 1):
                if direction == "U":
                        curY -= 1
                elif direction == "D":
                        curY += 1
                elif direction == "R":
                        curX += 1
                elif direction == "L":
                        curX -= 1
                moveTail()
    #printArray(tail)
    
    countMovement(tail)

def countMovement(arr):
    counter = 0
    for i in range(0, len(arr[0]), 1):
        for j in range(0, len(arr), 1):
            if arr[j][i] == "#" or arr[j][i] == "T":
                counter += 1

    print(counter + 1)

def moveTail():
    global curX, curY, tailX, tailY, num

    for i in range(0, len(num), 1):
        if i == 0:
            headX = curX
            headY = curY
        else:
            headX = tailX[i-1]
            headY = tailY[i-1]

        if i == len(num)-1: 
            tail[tailX[i]][tailY[i]] = "#"
        #Horizontal
        if abs(headX - tailX[i]) >1 and abs(headY - tailY[i]) == 0:
            if headX > tailX[i]:
                tailX[i] += 1
            else:
                tailX[i] -= 1
        #Vertical
        elif abs(headY - tailY[i]) >1 and abs(headX - tailX[i]) == 0:
            if headY > tailY[i]:
                tailY[i] += 1
            else:
                tailY[i] -= 1
        #Diagonal
        elif abs(headX - tailX[i]) > 1 and abs(headY - tailY[i]) > 0 or abs(headX - tailX[i]) > 0 and abs(headY - tailY[i]) > 1:
            if headX > tailX[i] and headY > tailY[i]: #2R1D, 1R2D
                tailX[i] += 1
                tailY[i] += 1
            elif headX > tailX[i] and headY < tailY[i]: # 2R1U, 1R2U
                tailX[i] += 1
                tailY[i] -= 1
            elif headX < tailX[i] and headY > tailY[i]: #2L1D, 1L2D
                tailX[i] -=1
                tailY[i] +=1
            elif headX < tailX[i] and headY < tailY[i]: #2L1U, 1L2U
                tailX[i] -=1
                tailY[i] -=1

    #printArray(visit)

def printArray(arr1):
    for i in range(0, len(arr1[0])):
        string = ""
        for j in range(0, len(arr1),1):
            string = string + arr1[j][i]
        print(string)
    print()

inputToArray()
moveHead()