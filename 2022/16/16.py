# Part 1: 2181, 2201, Part 2: x < 3332

import networkx as nx
import matplotlib.pyplot as plt

totalTime = 26

lines = []
valves = []
results = []
resArr = []

G = nx.Graph()  # unweighted Graph with 0 flowRate Edges
W = nx.Graph()  # weighted Graph without 0 flowrate weighted Edges

def inputToArray():
    global lines
    with open("input.txt", "r") as f:
        lines = f.readlines()

def parseInputToCoordinates(arr):
    for l in arr:
        split = l.split(sep=" ")
        # Valves: name, flow rate, [tunnels]
        valveName = split[1]
        flowRate = int(split[4][5:-1:])
        tunnels = []
        for tName in split[9::]:
            tunnels.append(tName[:-1:])
        valveIsOpen = False

        valves.append((valveName, flowRate, tunnels, valveIsOpen))

def inputToUnweightedGraph():
    for n, fr, tunnels, vio in valves:
        for t in tunnels:
            G.add_edge(n, t)
        G.nodes[n]["fr"] = fr

def CreateWeightedGraph():
    nonZeroNodes = []
    for n, fr, tunnels, vio in valves:
        if fr != 0 or n == "AA":
            nonZeroNodes.append((n, fr, tunnels, vio))
            W.add_node(n)
            W.nodes[n]["fr"] = fr
    
    nodeWeights = []
    for i in range(len(nonZeroNodes)-1):
        for j in range(i+1, len(nonZeroNodes)):
            nodeWeights.append((nonZeroNodes[i][0], nonZeroNodes[j][0], nx.shortest_path_length(G, nonZeroNodes[i][0], nonZeroNodes[j][0])))

    W.add_weighted_edges_from(nodeWeights)

def drawGraph(graph):
    node_color = [nx.get_node_attributes(graph, "fr")[i] for i in graph]
    plt.figure()
    pos_nodes = nx.spring_layout(graph)
    nx.draw_networkx(graph, pos_nodes, with_labels=True, node_color=node_color, alpha=0.7, node_size = 400)

    pos_attrs = {}
    for node, coords in pos_nodes.items():
        pos_attrs[node] = (coords[0], coords[1] + -0.08)

    node_attrs = nx.get_node_attributes(graph, 'fr')
    custom_node_attrs = {}
    for node, attr in node_attrs.items():
        if attr == 0:
            custom_node_attrs[node] = ""
        else:
            custom_node_attrs[node] = str(attr)

    nx.draw_networkx_labels(graph, pos_attrs, labels=custom_node_attrs)
    plt.show()

def traverseNeighbors(time, currentNode, visitedNodes):
    if time > 30:
        return
    
    visitedNodes.append(currentNode)
    for n in list(W.neighbors(currentNode)):
        if n in visitedNodes:
            continue
        
        newVisited = visitedNodes
        weight = W.get_edge_data(currentNode, n)["weight"]
        traverseNeighbors(time + weight + 1, n, newVisited)
    
    x = calculateRelease(visitedNodes)
    if x > 750:
        results.append(x)
        resArr.append(visitedNodes)
        print(str(visitedNodes) + ": " + str(x))

    visitedNodes.pop(visitedNodes.index(currentNode))

def calculateRelease(arr):
    index = 0
    release = 0
    floatValue = 0

    distance = W.get_edge_data(arr[index], arr[index + 1])["weight"]

    for time in range(totalTime):
        if time == distance:
            index +=1
            release += floatValue
            floatValue += W._node[arr[index]]["fr"]
            if index < len(arr)-1:
                distance += W.get_edge_data(arr[index], arr[index + 1])["weight"] + 1
            else:
                distance = 40
        else:
            release += floatValue

    return release

def findBestRoute(startNode):
    possiblePaths =[]
    time = 0
    for n in list(W.neighbors(startNode)):
        visitedNodes = [startNode]
        weight = W.get_edge_data(startNode, n)["weight"]
        traverseNeighbors(time + weight + 1, n, visitedNodes)

    print(max(results))

def checkForTwoActors():
    resis = []
    for i in range(len(resArr)-1):
        for j in range(len(resArr)):
            test = resArr[i] + resArr[j]
            test = set(test)
            if len(test) == len(resArr[i]) + len(resArr[j])-1:
                resis.append((results[i]+results[j], i, j))

    t = max(resis)
    print(t)
    print(resArr[t[1]])
    print(resArr[t[2]])

inputToArray()
parseInputToCoordinates(lines)
inputToUnweightedGraph()
CreateWeightedGraph()
findBestRoute("AA")
#drawGraph(G)
checkForTwoActors()