# Part 1: 17490, Part 2: 1.632.917.375.836

lines = []
arrayToSort = []

def InputToArray():
    global lines
    with open("input.txt", "r") as f:
        lines = f.read().splitlines()


def part1():
    arrayToSort = []
    for i in range(len(lines)):
        arrayToSort.append((int(lines[i]), i))

    for i in range(len(lines)):
        elem = [item for item in arrayToSort if item[1] == i]
        pos = arrayToSort.index(elem[0])
        arrayToSort.pop(arrayToSort.index(elem[0]))
        arrayToSort.insert((pos + elem[0][0]) % len(arrayToSort), elem[0])

    elem = [item for item in arrayToSort if item[0] == 0]
    pos = arrayToSort.index(elem[0])

    print(sum([arrayToSort[(pos + item) % len(arrayToSort)][0] for item in [1000, 2000, 3000]]))
    
def part2():
    arrayToSort = []
    for i in range(len(lines)):
        arrayToSort.append((int(lines[i])*811589153, i))

    for j in range(10):
        for i in range(len(lines)):
            elem = [item for item in arrayToSort if item[1] == i]
            pos = arrayToSort.index(elem[0])
            arrayToSort.pop(arrayToSort.index(elem[0]))
            arrayToSort.insert((pos + elem[0][0]) % len(arrayToSort), elem[0])

        elem = [item for item in arrayToSort if item[0] == 0]
        pos = arrayToSort.index(elem[0])

    print(sum([arrayToSort[(pos + item) % len(arrayToSort)][0] for item in [1000, 2000, 3000]]))

InputToArray()
part1()
part2()