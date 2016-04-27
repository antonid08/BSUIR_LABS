class MyVector(object):
    def __init__(self, elems=[]):
        if isinstance(elems, (list, tuple)):
            self.mainList = elems
        else:
            raise TypeError

    def add(self, value):
        if isinstance(value, (list, tuple)):
            self.mainList += value
        elif isinstance(value, (int, float)):
            self.mainList.append(value)
        else:
            raise TypeError

    def __add__(self, other):
        return MyVector(map(lambda x, y: (x or 0) + (y or 0), self.mainList,
                            other.mainList))

    def __sub__(self, other):
        return MyVector(map(lambda x, y: (x or 0) - (y or 0), self.mainList,
                            other.mainList))

    def __mul__(self, other):
        if isinstance(other, (int, float)):
            return MyVector([x * other for x in self.mainList])
        elif isinstance(other, MyVector):
            return MyVector(map(lambda x, y: (x or 1) * (y or 1), self.mainList,
                                other.mainList))
        raise TypeError

    def __eq__(self, other):
        try:
            return (True if self.mainList == other.mainList else False)
        except Exception:
            raise TypeError

    def __getitem__(self, index):
        return self.mainList[index]

    def size(self):
        return len(self.mainList)

    def __str__(self):
        return ' '.join(str(elem) for elem in self.mainList)


def main():
    v1 = MyVector([1, 2, 3])
    # v2 = MyVector([2, 3])

    print str(v1.size())

if __name__ == '__main__':
    main()
