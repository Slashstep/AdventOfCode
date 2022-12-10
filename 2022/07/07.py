from anytree import NodeMixin, RenderTree, PostOrderIter, search

class MyBaseClass(object):  # Just an example of a base class
    foo = 4
class Node(MyBaseClass, NodeMixin):  # Add Node feature
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
        #print(l)
        if lines[l][0:1:1] != "$":
            if lines[l][0:3:1] == "dir":
                l = Node(lines[l][4:-1], 0, parent=par, children=None)
            else:
                size, name = lines[l].split(sep=" ", maxsplit=1)
                l = Node(name[:-1:1], int(size), parent=par, children=None)
                uPar = par
                while par != None:
                    par.size = par.size + int(size)
                    par = par.parent
                par = uPar
        elif lines[l] == "$ cd ..\n":
            par = par.parent
        elif lines[l] != "$ ls\n":
            for d in par.children:
                if d.name == lines[l][5:-1:1]:
                    par = d
    
    for pre, _, node in RenderTree(c):
        treestr = u"%s%s" % (pre, node.name)
        print(treestr.ljust(8), node.size)

    nodeListe = list(PostOrderIter(c, filter_=lambda node: node.children != ()))

    sizeListe = []
    for n in nodeListe:
        sizeListe.append(n.size)

    print(len(sizeListe))
    sizeListe.sort()

    difTotal = 70000000 - c.size
    difUpdate = 30000000 - difTotal
    for s in sizeListe:
        if s > difUpdate:
            print(s)
            break
        


CreatDictionary()
