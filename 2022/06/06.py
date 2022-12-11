# Part 1: 1912, Part 2: 2122

lines = []

def inputToArray():
    global lines
    with open('input.txt', 'r') as f:
        lines = f.readlines()

def detectDiffs(amount):
    c = []

    for i in range(amount-1, len(lines[0]), 1):
        for l in range(0, amount, 1):
            c.append(lines[0][i-l])

        if len(set(c)) == len(c):
            print(i+1)
            break
        
        c.clear()
        
inputToArray()
detectDiffs(4)    # Part 1
detectDiffs(14)   # Part 2
