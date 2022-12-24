# Part 1: 142.707.821.472.432 Part 2: #### soll 3.587.647.562.851

lines = []
sArr = []

def InputToArray():
    global lines, sArr
    with open("input.txt", "r") as f:
        lines = f.read().splitlines()

    for l in lines:
        varName, formula = l.split(sep=": ", maxsplit=1)

        sArr.append((varName, formula))

def VarFinder(vName):
    elem = [item for item in sArr if item[0] == vName]

    s = elem[0][1].split(sep=" ")
    
    if len(s) == 1:
        return int(s[0])
    else:
        locals()[s[0]] = VarFinder(s[0])
        locals()[s[2]] = VarFinder(s[2])
        return eval(elem[0][1])

def FindHuman(vName):
    if vName == "humn":
        return vName
    else:
        elem = [item for item in sArr if item[0] == vName]

        s = elem[0][1].split(sep=" ")
        
        if len(s) > 1:
            locals()[s[0]] = FindHuman(s[0])
            locals()[s[2]] = FindHuman(s[2])
        else:
            return

    return vName

def ChangeHuman(vName, diference):
    # Find Var mit Humn
    elem = [item for item in sArr if item[0] == vName]

    s = elem[0][1].split(sep=" ")

    if len(s) > 1:
        if FindHuman(s[0]) != None:
            hVar = FindHuman(s[0])
            nhVar = FindHuman(s[2])
        else:
            hVar = FindHuman(s[2])
            nhVar = FindHuman(s[0])

    # Calculate difference between hVar and nhVar

    dif = int(VarFinder(nhVar) - VarFinder(hVar))

    print(VarFinder(hVar))
    print(VarFinder(nhVar))
    print(dif)
            

InputToArray()
print(int(VarFinder("root")))   #Part 1
ChangeHuman("root", 0)          #Part 2