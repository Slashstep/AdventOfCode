# Part 1: 1692 Part 2: 1724
from sys import maxsize

lines = []

def inputToArray():
    global lines
    with open("input.txt", "r") as f:
        lines = f.readlines()

def countIncreases(arr):
    counter = 0
    current = maxsize

    for l in arr:
        if int(l) > current:
            counter += 1
            current = int(l)
        else:
            current = int(l)
    
    print(counter)

def countThreeIncrease(arr):
    sumArr = []

    for i in range(0, len(arr)-2, 1):
        sumArr.append(int(arr[i]) + int(arr[i+1]) + int(arr[i+2]))

    countIncreases(sumArr)

inputToArray()
countIncreases(lines)
countThreeIncrease(lines)