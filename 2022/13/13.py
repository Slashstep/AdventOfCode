# Part 1: 6046,  Part 2: 21423

lines = []

def inputToArray():
    global lines
    with open("input.txt", "r") as f:
        lines = f.readlines()

def checkPairs(arr):
    pairs = []
    for l in arr:
        if l != "\n":
            pairs.append(eval(l))

    summe = 0
    for k in range(0, len(pairs), 2):
        left = pairs[k]
        right = pairs[k+1]

        currentPair = zip(left, right)
        erg = 10
        for i, p in enumerate(currentPair):
            erg = isInRightOrder(p[0], p[1])
            if erg < 0:
                summe += (k+2)/2
                break
            elif erg == 1:
                break

        if len(list(currentPair)) == 0 and erg == 10 or erg == 0:
            if len(left) < len(right):
                summe += (k+2)/2

    print(int(summe))

def checkPairsSorted(arr):
    pairs = []
    for l in arr:
        if l != "\n":
            pairs.append(eval(l))

    pairs.append([[2]])
    pairs.append([[6]])

    isRightOrder = False
    for n in range(len(pairs), 0, -1):
        for m in range(0, n-1, 1):

            left = pairs[m]
            right = pairs[m+1]

            currentPair = zip(left, right)
            erg = 10
            for i, p in enumerate(currentPair):
                erg = isInRightOrder(p[0], p[1])
                if erg < 0:
                    break
                elif erg == 1:
                    pairs[m], pairs[m+1] = pairs[m+1], pairs[m]
                    break

            if len(list(currentPair)) == 0 and erg == 10 or erg == 0:
                if len(left) < len(right):
                    isRightOrder = True
                else:
                    pairs[m], pairs[m+1] = pairs[m+1], pairs[m]

    for l in pairs:
        print(l)

    pos2 = pairs.index([[2]]) + 1
    pos6 = pairs.index([[6]]) + 1
    print(pos2*pos6)

def isInRightOrder(left, right):
    if left == None:
        return -1
    elif right == None:
        return 1
    if type(left) == int and type(right) == int:
        if left < right:
            return -1
        elif left == right:
            return 0
        elif left > right:
            return 1
    elif type(left) == int:
        left = [left]
    elif type(right) == int:
        right = [right]

    t = zip(left, right)
    test = 10
    for i, p in enumerate(t):
        test = isInRightOrder(p[0], p[1])
        if test != 10 and test != 0 and test != None:
            return test
    if test == 10 or test == 0:
            if len(left) < len(right):
                return -1
            if len(left) == len(right):
                return 0
            else:
                return 1
    return 1

inputToArray()
checkPairs(lines)
checkPairsSorted(lines)

