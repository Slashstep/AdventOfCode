# A - Rock
# B - Paper
# C - Scissors
# X - Rock - 1
# Y - Paper - 2
# Z - Scissors - 3
# Loose - 0
# Draw - 3
# Win - 6
# PART 2
# X - Loose - 0
# Y - Draw 3
# Z - Win - 6

def getTotalStratScore():
    with open('input.txt', 'r') as f:
        lines = f.readlines()

    sum = 0
    x = 1
    y = 2
    z = 3
    draw = 3
    win = 6
    for l in lines:
        if(l == "A X\n"):
            sum = sum + x + draw
        elif(l== "A Y\n"):
            sum = sum + y + win
        elif(l == "A Z\n"):
            sum = sum + z
        elif(l == "B X\n"):
            sum = sum + x
        elif(l == "B Y\n"):
            sum = sum + y + draw
        elif(l == "B Z\n"):
            sum = sum + z + win
        elif(l == "C X\n"):
            sum = sum + x + win
        elif(l == "C Y\n"):
            sum = sum + y
        elif(l == "C Z\n"):
            sum = sum + z + draw
    
    print(sum)

def getTotalStratScore2():
    with open('input.txt', 'r') as f:
        lines = f.readlines()

    sum = 0
    x = 1
    y = 2
    z = 3
    draw = 3
    win = 6
    for l in lines:
        if(l == "A X\n"):
            sum = sum + z
        elif(l== "A Y\n"):
            sum = sum + x + draw
        elif(l == "A Z\n"):
            sum = sum + y + win
        elif(l == "B X\n"):
            sum = sum + x
        elif(l == "B Y\n"):
            sum = sum + y + draw
        elif(l == "B Z\n"):
            sum = sum + z + win
        elif(l == "C X\n"):
            sum = sum + y
        elif(l == "C Y\n"):
            sum = sum + z + draw
        elif(l == "C Z\n"):
            sum = sum + x + win
    
    print(sum)

getTotalStratScore2()