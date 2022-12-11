# Part 1: 13005, Part 2: 11373
# A - Rock, B - Paper, C - Scissors
# X - Rock - 1, Y - Paper - 2, Z - Scissors - 3
# Loose - 0, Draw - 3, Win - 6
# PART 2
# X - Loose - 0, Y - Draw 3, Z - Win - 6

lines = []

def inputToArray():
    global lines
    with open('input.txt', 'r') as f:
        lines = f.readlines()

def getTotalStratScore():
    sum = 0
    x = 1
    y = 2
    z = 3
    draw = 3
    win = 6
    for l in lines:
        match l:
            case "A X\n":
                sum = sum + x + draw
            case "A Y\n":
                sum = sum + y + win
            case "A Z\n":
                sum = sum + z
            case "B X\n":
                sum = sum + x
            case "B Y\n":
                sum = sum + y + draw
            case "B Z\n":
                sum = sum + z + win
            case "C X\n":
                sum = sum + x + win
            case "C Y\n":
                sum = sum + y
            case "C Z\n":
                sum = sum + z + draw
    
    print(sum)

def getTotalStratScore2():
    sum = 0
    x = 1
    y = 2
    z = 3
    draw = 3
    win = 6
    for l in lines:
        match l:
            case "A X\n":
                sum = sum + z
            case "A Y\n":
                sum = sum + x + draw
            case "A Z\n":
                sum = sum + y + win
            case "B X\n":
                sum = sum + x
            case "B Y\n":
                sum = sum + y + draw
            case "B Z\n":
                sum = sum + z + win
            case "C X\n":
                sum = sum + y
            case "C Y\n":
                sum = sum + z + draw
            case "C Z\n":
                sum = sum + x + win
    
    print(sum)

inputToArray()
getTotalStratScore()    # Part 1
getTotalStratScore2()   # Part 2