# Part 1: 490 Part 2: 488
x = 173
y = 41

class Node():
    def __init__(self, value, parent=None, position=None):
        self.value = value
        self.position = position
        self.parent = parent

grid = [["a" for row in range(0, x)] for col in range(0, y)]

startX = 0
startY = 0
targetX = 0
targetY = 0

def inputToArray():
    global startX, startY, targetX, targetY, grid
    with open("input.txt", "r") as f:
        lines = f.readlines()

    for i in range(0, len(lines), 1):
        for j in range(0, len(lines[0])-1, 1):
            grid[i][j] = lines[i][j:j+1]
            match lines[i][j:j+1]:
                case "S":
                    startX = j
                    startY = i
                    grid[i][j] = "a"
                case "E":
                    targetX = j
                    targetY = i
                    grid[i][j] = "z"

def checkEachA():
    pathlen = []
    for i in range(0, len(grid), 1):
        for j in range(165, len(grid[0]), 1):
            if grid[i][j] == "a":
                start = Node("a", None, (i, j))

                pathlen.append(wayfinder(start))

    pathlen.sort()
    print(pathlen[0])

def wayfinder(start_node):
    global target_Node
    #Starting Node to open List

    openSet = []
    closedSet = []
    
    openSet.append(start_node)

    while len(openSet) > 0:
        current_node = openSet[0]

        # Pop current off openSet, add to closedSet
        openSet.pop(0)
        closedSet.append(current_node)
        
        # Found goal
        if current_node.position == target_Node.position:
            path = []
            current = current_node

            print("Target Found")

            while current is not None:
                path.append(current.position)
                current = current.parent
            print(len(path) - 1)
            return len(path)-1

        # Generate children
        for new_position in [(-1, 0), (0, 1), (1, 0), (0, -1)]:

            # Get Node position
            node_position = (current_node.position[0] + new_position[0], current_node.position[1] + new_position[1])

            # Clamp to range
            if node_position[0] < 0 or node_position[0] > len(grid[0]) - 1 or node_position[1] < 0 or node_position[1] > len(grid) -1:
                continue

            #Make sure letters match
            dist = ord(grid[node_position[1]][node_position[0]]) - ord(current_node.value)

            if dist >1:
                continue

            isBreak = False

            for closed_child in closedSet:
                if node_position == closed_child.position:
                    isBreak = True

            if isBreak == True:
                continue
                
            for open_node in openSet:
                if node_position == open_node.position:
                    isBreak = True

            if isBreak == True:
                continue
            
            new_node = Node(grid[node_position[1]][node_position[0]], current_node, node_position)
            
            openSet.append(new_node)

def wayfinderback(start_node):
    #Starting Node to open List

    openSet = []
    closedSet = []
    
    openSet.append(start_node)

    while len(openSet) > 0:
        current_node = openSet[0]

        # Pop current off openSet, add to closedSet
        openSet.pop(0)
        closedSet.append(current_node)
        
        # Found goal
        if current_node.value == "a":
            path = []
            current = current_node

            print("Target Found")

            while current is not None:
                path.append(current.position)
                current = current.parent
            print(len(path) - 1)
            return len(path)-1

        # Generate children
        for new_position in [(-1, 0), (0, 1), (1, 0), (0, -1)]:

            # Get Node position
            node_position = (current_node.position[0] + new_position[0], current_node.position[1] + new_position[1])

            # Clamp to range
            if node_position[0] < 0 or node_position[0] > len(grid[0]) - 1 or node_position[1] < 0 or node_position[1] > len(grid) -1:
                continue

            #Make sure letters match
            dist = ord(current_node.value) - ord(grid[node_position[1]][node_position[0]])

            if dist >1:
                continue

            isBreak = False

            for closed_child in closedSet:
                if node_position == closed_child.position:
                    isBreak = True

            if isBreak == True:
                continue
                
            for open_node in openSet:
                if node_position == open_node.position:
                    isBreak = True

            if isBreak == True:
                continue
            
            new_node = Node(grid[node_position[1]][node_position[0]], current_node, node_position)
            
            openSet.append(new_node)

def printArray(arr1):
    for i in range(0, len(arr1)):
        string = ""
        for j in range(0, len(arr1[0]),1):
            string = string + arr1[i][j]
        print(string)
    print()

inputToArray() 
start_node = Node("a", None, (startX, startY))
target_Node = Node("z", None, (targetX, targetY))
wayfinderback(start_node)
wayfinderback(target_Node)
#checkEachA()