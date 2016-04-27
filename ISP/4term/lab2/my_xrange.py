def myXrange(finish, start=0, step=1):
    if step == 0:
        raise ValueError('Step can\' t be 0')

    while start <= finish:
        yield start
        start += step

def main():
    for item in myXrange(3):
        print item
    print ''

    for item in myXrange(10, 2):
        print item
    print ''

    for item in myXrange(10, 2, 3):
        print item

if __name__ == '__main__':
    main()
