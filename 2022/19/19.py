# Part 1: ####, Part 2: ####
# ID*geoden

lines = []
blueprints = []

ore = 0
clay = 0
obsedian = 0
geode = 0

oreBot = 1
clayBot = 0
obsedianBot = 0
geodeBot = 0

def InputToArray():
    global lines
    with open("input.txt", "r") as f:
        lines = f.readlines()

def ParseInputToCoordinates():
    for l in lines:
        split = l.split(sep=" ")
        blueprints.append((int(split[6]), int(split[12]), int(split[18]), int(split[21]), int(split[27]), int(split[30])))

def RiseOfTheRobots(time):
    ore = 0
    clay = 0
    obsedian = 0
    geode = 0

    oreBot = 1
    clayBot = 0
    obsedianBot = 0
    geodeBot = 0

    for i in range(time):
        ore += oreBot
        clay += clayBot
        obsedian += obsedianBot
        geode += geodeBot

        if ore == blueprints[0][4] and obsedian == blueprints[0][5]:
            ore -= blueprints[0][4]
            obsedian -= blueprints[0][5]
            geodeBot += 1
        elif ore == blueprints[0][2] and clay == blueprints[0][3]:
            ore -= blueprints[0][2]
            clay -= blueprints[0][3]
            obsedianBot += 1
        elif ore == blueprints[0][1]:
            ore -= blueprints[0][1]
            clayBot += 1
        elif ore == blueprints[0][0]:
            ore -= blueprints[0][0]
            oreBot += 1

    print(ore)
    print(clay)
    print(obsedian)
    print(geode)


InputToArray()
ParseInputToCoordinates()
RiseOfTheRobots(24)