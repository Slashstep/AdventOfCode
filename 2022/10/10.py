#Part 1: 14540, Part 2: EHZFZHCZ
#TODO: Modulo Calculation to be fixed that last col becomes first col

lines = []

def inputToArray():
    global lines
    with open("input.txt", "r") as f:
        lines = f.readlines()

def cycle():
    counter = 0
    sum = 0
    register = 1
    numberToAdd = 0
    isAdding = False
    string = ""
    for i in range(1, 241, 1):
        if (i+20) % 40 == 0:
            sum = sum + register*i
        
        if isAdding == True:
            register += int(numberToAdd)
            isAdding = False
            numberToAdd = 0
            counter += 1
        else:
            if lines[counter] != "noop\n":
                operation, amount = lines[counter].split(sep=" ", maxsplit=1)
                numberToAdd = int(amount)
                isAdding = True
            else:
                isAdding = False
                counter += 1
        if register == (i) % 40 or register == (i) % 40 - 1 or register == (i) % 40 +1:
            string += "#"
        else:
            string += " "
        if i % 40 == 0:
            print(string)
            string = ""
    print(str(i) + ": " + str(register) + ", sum: " + str(sum) + ", n2a: " + str(numberToAdd))

inputToArray()
cycle()