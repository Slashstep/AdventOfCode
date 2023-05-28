# Part 1: ####, Part 2:

lines = []

def InputToArray():
    global lines, karte
    with open("input.txt", "r") as f:
        lines = f.read().splitlines()

def SNAFU2DEC(potence, number):
    match number:
        case "0":
            return 0 * 5 ** potence
        case "1":
            return 1 * 5 ** potence
        case "2":
            return 2 * 5 ** potence
        case "-":
            return -1 * 5 ** potence
        case "=":
            return -2 * 5 ** potence

def DEC2SNAFU():
    print()

def part1(arr):
    sum = 0
    for l in arr:
        decimalNum = 0
        length = len(l) - 1

        for i, n in enumerate(l):
            decimalNum += SNAFU2DEC(length - i, n)

        sum += decimalNum

    print(sum)
    # 29 541007400367
    # 2=--=0000-1-0-=1=0=2

def AddNums(arr):
    sum = ["0" for row in range(20)]
    for l in arr:
        overlap = 0
        for i, n in enumerate(reversed(l)):
            if n == "-":
                n1 = 4
            elif n == "=":
                n1 = 3
            else: 
                n1 = int(n)

            if sum[i] == "-":
                n2 = 4
            elif sum[i] == "=":
                n2 = 3
            else:
                n2 = int(sum[i])

            dig = (n1 + n2 + overlap) % 5

            if n1 == 1 and n2 == 2 or n1 == 2 and n2 == 2 or n1 == 2 and n2 == 1:
                overlap = 1
            elif n1 == 3 and n2 == 4 or n1 == 3 and n2 == 3 or n1 == 4 and n2 == 3:
                overlap = -1
            else:
                overlap = 0

            if dig == 3:
                sum[i] = "="
            elif dig == 4:
                sum[i] = "-"
            else:
                sum[i] = str(dig)
            
    num = reversed(sum)
    string = ""
    for k in num:
        string += k

    print(string)
    print(part1([string]))

InputToArray()
part1(lines)
AddNums(lines)