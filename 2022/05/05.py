# Part 1: TLFGBZHCN, Part 2: QRQFHFWCL
# es sollen die Buchstaben gefunden werden, die nach der Operation oben sind

crates = [[] for col in range(0,10)]

def putCratesInLists():
    with open('crates.txt', 'r') as f:
        lines = f.readlines()
    
    for j in range(0, len(lines)-1, 1):
        for i in range(0, len(lines[j])-3, 4):
            box = lines[j][i:i+3:1]
            if box != "   " and box != "  \n":
                crates[int(i/4)].append(box)

    craneOperations()

def craneOperations():
    with open('input.txt', 'r') as f:
        lines = f.readlines()

    crane = []
    for l in lines:
        splitarr =l.split()
        splitarr.pop(0)
        splitarr.pop(1)
        splitarr.pop(2)
        crane.append(splitarr)

    for c in crane:
        #moveCrates9000(int(c[0]), int(c[1]), int(c[2]))    # Part 1
        moveCrates9001(int(c[0]), int(c[1]), int(c[2]))     # Part 2

    findUpperCrate()

def moveCrates9000(amount, start, target):
    for i in range(0, amount, 1):
        crates[target -1 ].insert(0, crates[start - 1][0])
        crates[start - 1].pop(0)

def moveCrates9001(amount, start, target):
    for i in range(0, amount, 1):
        crates[target -1 ].insert(0, crates[start - 1][amount - 1 - i])
    for i in range(0, amount, 1):
        crates[start -1].pop(0)

def findUpperCrate():
    key = ""
    for c in crates:
        for i in range(0, len(c), 1):
            if c[i] != "":
                key += c[i]
                break
    print(key)

putCratesInLists()