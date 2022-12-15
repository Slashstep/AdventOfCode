# Part 1: ####, Part 2: ####

# Find the most commen and the least commen binary number, translate to decimal and multiply

lines = []

def inputToArray():
    global lines
    with open("input.txt", "r") as f:
        lines = f.readlines()

def findNumbers():
    mCom = ""
    lCom = ""
    for i in range(0, len(lines[0])-1, 1):
        count0 = 0
        count1 = 0
        for j in range(0, len(lines), 1):
            if lines[j][i:i+1] == "0":
                count0 += 1
            else:
                count1 += 1

        if count0 > count1:
            mCom += "0"
            lCom += "1"
        else:
            mCom += "1"
            lCom += "0"

    print(mCom)
    print(lCom)

    mComDec = int(mCom, 2)
    lComDec = int(lCom, 2)

    print(mComDec * lComDec)

    o2 = lines
    co2 = lines

    for l in range(0, len(o2[0]-1), 1):
        for k in range(0, len(o2), 1):
            if o2[k][l:l+1] != mCom[l:l+1]:
                o2.pop(k)


inputToArray()
findNumbers()