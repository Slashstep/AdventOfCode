# Part 1: ####, Part 2: ####

lines = []
karte = []

def InputToArray():
    global lines, karte
    with open("input.txt", "r") as f:
        lines = f.read().splitlines()

    for l in range(len(lines)):
        karte.append([])
        for k in range(len(lines[l])):
            karte[l].append(((0 if lines[l][k:k+1:1] == "." else 1), (0,0)))


def MoveElfes():
    

InputToArray()
    

