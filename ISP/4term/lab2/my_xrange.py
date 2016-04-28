class My_xrange(object):
    def __init__(self, finish, start=0, step=1):
        self.finish = finish
        self.start = start
        self.step = step
        self.pointer = 0
        self.current_value = start

    def __iter__(self):
        return self

    def next(self):
        if self.current_value < self.finish:
            try:
                return self.current_value
            finally:
                self.current_value += self.step
        else:
            self.current_value = self.start
            raise StopIteration

    def __getitem__(self, index):
        return self.start + self.step * index


def main():
    a = My_xrange(3)
    print 'First loop (a):'
    for item in a:
        print item
    print ''

    print 'Second loop (a):'
    for item in a:
        print item

    for item in My_xrange(10, 2):
        print item
    print ''

    print My_xrange(10, 2, 2)[2]

    print next(reversed(xrange(100500)))

if __name__ == '__main__':
    main()
