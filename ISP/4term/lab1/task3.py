store = set()
command = raw_input('>>').split(' ')

while command[0] != 'exit':
    if command[0] == 'add':
        store.update(command[1:])

    elif command[0] == 'remove':
        try:
            store.remove(command[1])
        except KeyError:
            print 'There is no such key'

    elif command[0] == 'find':
        for arg in command[1:]:
            if arg in store:
                print arg
            else:
                print arg + ' - not founded'

    elif command[0] == 'list':
        if store:
            for elem in store:
                print elem
        else:
            print ('Store is empty')

    elif command[0] != 'exit':
        print 'Illegal command!'

    command = raw_input('>>').split(' ')
