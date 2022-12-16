# Part 1: x < 4424278 Part 2: 10382630753392

from math import inf

limitRight = 4000000

lines = []
sensoryCor = []
beaconCor = []
sbDistances = []

def inputToArray():
    global lines
    with open("input.txt", "r") as f:
        lines = f.readlines()

def parseInputToCoordinates(arr):
    for l in arr:
        split = l.split(sep=" ")
        
        sensoryCor.append((int(split[2][2:-1:]), int(split[3][2:-1:])))
        beaconCor.append((int(split[8][2:-1:]), int(split[9][2:-1:])))

def dist(x1, x2, y1, y2):
    return abs(x1-x2) + abs(y1 - y2)

def calculateSensorBeaconDistance(sensors, beacons):
    for i, sensor in enumerate(sensors):
        sbDistances.append(dist(sensor[0], beacons[i][0], sensor[1], beacons[i][1]))

def calculateLineCovarage(line, sensors, distances):
    minX, maxX = inf, -inf
    for i in range(0, len(distances), 1):
        yDist = abs(sensors[i][1]-line)
        if distances[i] >= yDist:
            restDist = abs(distances[i] - yDist)
            xLeft = sensors[i][0]-restDist
            xRight = sensors[i][0]+restDist

            minX = min(xLeft, minX)
            maxX = max(xRight, maxX)

    return maxX-minX

def checkEachLine(i):
    coveredRanges = []
    for j in range(0, len(sbDistances), 1):
        yDist = abs(sensoryCor[j][1]-i)
        if yDist > sbDistances[j]:
            continue
        restDist = sbDistances[j] - yDist
        xLeft = sensoryCor[j][0]-restDist
        xRight = sensoryCor[j][0]+restDist
        
        coveredRanges.append((xLeft, xRight))
    coveredRanges.sort()

    curMaxRight = coveredRanges[0][1]
    for x in range(0, len(coveredRanges), 1):
        if coveredRanges[x][0] > curMaxRight:
            return curMaxRight + 1
        curMaxRight = max(curMaxRight, coveredRanges[x][1])
    
    return False

def checkPart2():
    for i in range(0, limitRight, 1):
        x = checkEachLine(i)
        if x != False:
            print(x * 4000000 + i)
            break

inputToArray()
parseInputToCoordinates(lines)
calculateSensorBeaconDistance(sensoryCor, beaconCor)
print(calculateLineCovarage(2000000, sensoryCor, sbDistances))
checkPart2()
