# Part 1: 1746616 Part 2: 1741971043

lines = []

def inputToArray():
    global lines
    with open("input.txt", "r") as f:
        lines = f.readlines()

def submarinePart1(arr):
    horizontal = 0
    depth = 0

    for l in arr:
        direction, amount = l.split(sep=" ", maxsplit=1)
        match direction:
            case "forward":
                horizontal += int(amount)
            case "up":
                depth -= int(amount)
            case "down":
                depth += int(amount)

    print(horizontal*depth)

def submarinePart2(arr):
    horizontal = 0
    depth = 0
    aim = 0

    for l in arr:
        direction, amount = l.split(sep=" ", maxsplit=1)
        match direction:
            case "forward":
                horizontal += int(amount)
                depth += (aim * int(amount))
            case "up":
                aim -= int(amount)
            case "down":
                aim += int(amount)

    print(horizontal*depth)

inputToArray()
submarinePart1(lines)
submarinePart2(lines)