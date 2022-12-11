# Part 1: 111210, Part 2: 15447387620
# Part 2 uses the least common multiplyer for all the test values to keep the items small, 
# cause you can savely remove that value from all items before handing them over
# Monkeys were hard coded, for a parsed solution look somewhere else ;)

class Monkey():
    def __init__(self, identifyer, items, operation, test, monkeyTrue, monkeyFalse):
        self.identifyer = identifyer #Int that identifies the monkey
        self.items = items
        self.operation = operation
        self.test = test
        self.monkeyTrue = monkeyTrue
        self.monkeyFalse = monkeyFalse
        self.checks = 0

m0 = Monkey(0, [54, 53], "old * 3", 2, 2, 6)
m1 = Monkey(1, [95, 88, 75, 81, 91, 67, 65, 84], "old * 11", 7, 3, 4)
m2 = Monkey(2, [76, 81, 50, 93, 96, 81, 83], "old + 6", 3, 5, 1)
m3 = Monkey(3, [83, 85, 85, 63], "old + 4", 11, 7, 4)
m4 = Monkey(4, [85, 52, 64], "old + 8", 17, 0, 7)
m5 = Monkey(5, [57], "old + 2", 5, 1, 3)
m6 = Monkey(6, [60, 95, 76, 66, 91], "old * old", 13, 2, 5)
m7 = Monkey(7, [65, 84, 76, 72, 79, 65], "old + 5", 19, 6, 0)

monkeys = [m0, m1, m2, m3, m4, m5, m6, m7]
least = 1

def calcLCM():
    global least
    for m in monkeys:
        least *= m.test

def KeepAway(iterations, divideBy3):
    for i in range(0, iterations, 1):
        for monk in monkeys:
            for item in monk.items:
                old = item
                new = eval(monk.operation)
                new = new // 3 if divideBy3 else new // 1
                new = new % least

                if new % monk.test == 0:
                    monkeys[monk.monkeyTrue].items.insert(0, new)
                else:
                    monkeys[monk.monkeyFalse].items.insert(0, new)

                monk.checks += 1
            monk.items.clear()

    checks = []
    for m in monkeys:
        print(str(m.identifyer) + ": " + str(m.checks))
        checks.append(m.checks)

    checks.sort()
    print(checks[-1] * checks[-2])
    
calcLCM()
KeepAway(20, True)          # Part 1
#KeepAway(10000, False)     # Part 2
