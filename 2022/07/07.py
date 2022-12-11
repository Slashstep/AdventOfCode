# Part 1: 1391690, Part 2: 5469168
from anytree import NodeMixin, RenderTree, PostOrderIter, search

class Node(NodeMixin):  # Add Node feature
     def __init__(self, name, size, parent=None, children=None):
         super(Node, self).__init__()
         self.name = name
         self.size = int(size)
         self.parent = parent
         if children:
            self.children = children

def CreatDictionary():
    with open('input.txt', 'r') as f:
        lines = f.readlines()
        
    c = Node("c", 0)
    par = c
    
    for l in range(1, len(lines), 1):
        if lines[l][0:1] != "$":
            if lines[l][0:3] == "dir":
                l = Node(lines[l][4:-1], 0, parent=par, children=None)
            else:
                size, name = lines[l].split(sep=" ", maxsplit=1)
                uPar = par
                while par != None:
                    par.size += int(size)
                    par = par.parent
                par = uPar
        elif lines[l] == "$ cd ..\n":
            par = par.parent
        elif lines[l] != "$ ls\n":
            for d in par.children:
                if d.name == lines[l][5:-1]:
                    par = d
    
    for pre, _, node in RenderTree(c):
        treestr = u"%s%s" % (pre, node.name)
        print(treestr.ljust(8), node.size)

    nodeListe = list(PostOrderIter(c))

    sizeListe = []
    for n in nodeListe:
        sizeListe.append(n.size)

    sizeListe.sort()

    # Part 1:
    sum = 0
    for s in sizeListe:
        if s <= 100000:
            sum += s
    print(sum)

    # Part 2:
    difTotal = 70000000 - c.size
    difUpdate = 30000000 - difTotal
    for s in sizeListe:
        if s > difUpdate:
            print(s)
            break
        
CreatDictionary()
