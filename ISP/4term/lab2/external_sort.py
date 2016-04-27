import tempfile as tf
import argparse
from operator import itemgetter


def sort_buffer(args, buff):
    temp_file = tf.TemporaryFile()

    if args.key:
        sorted_list = sorted(buff[:-1].split(args.line_separator),
                             key=itemgetter(*args.key),
                             reverse=args.reverse)
    else:
        sorted_list = sorted(buff[:-1].split(args.line_separator),
                             reverse=args.reverse)

    sorted_buffer = '\n'.join(sorted_list)
    sorted_buffer += '\n'

    temp_file.write(sorted_buffer)
    temp_file.seek(0)

    return temp_file


def get_sorted_temp_files(args, unsorted_file, buffer_size):
    temp_files = []

    while True:
        buff = unsorted_file.read()
        if not buff:
            break
        temp_files.append(sort_buffer(args, buff))
    return temp_files


def read_line(f, eol):
    symbols = []

    current_symbol = f.read(1)
    while current_symbol != eol and current_symbol != '':
        symbols.append(current_symbol)
        current_symbol = f.read(1)
    symbols.append(current_symbol)

    line = ''.join(symbols)

    return line


def merge(args, temp_files):
    def merge_two(left_file, rigth_file):
        result = tf.TemporaryFile()
        rigth_str = read_line(rigth_file, args.line_separator)
        left_str = read_line(left_file, args.line_separator)

        while min([len(left_str), len(rigth_str)]):
            compare_condition = (left_str > rigth_str if args.reserve else
                                 left_str < rigth_str)
            if compare_condition:
                result.write(left_str)
                left_str = read_line(left_file, args.line_separator)
            else:
                result.write(rigth_str)
                rigth_str = read_line(rigth_file, args.line_separator)

        rest_file = left_file if len(left_str) else rigth_file

        line = left_str if len(left_str) else rigth_str
        while len(line):
            result.write(line)
            line = read_line(rest_file, args.line_separator)

        result.seek(0)
        return result

    if len(temp_files) == 2:
        return merge_two(temp_files[0], temp_files[1])
    if len(temp_files) == 1:
        return temp_files[0]

    return merge([merge(temp_files[:len(temp_files) / 2]),
                  merge(temp_files[len(temp_files) / 2:])])


def check(args):
    def get_symbols_from_str(string, numbers_of_symbols):
        try:
            symbols = [string[x] for x in numbers_of_symbols]
        except IndexError:
            if not len(string):
                return ''
            else:
                raise IndexError('''Use numbers from 0 to
                                 (length of string - 1).''')
        return ''.join(symbols)

    def is_condition_execute(first_line, second_line):
        if args.key:
            result = (get_symbols_from_str(first_line, args.key) <=
                      get_symbols_from_str(second_line, args.key))
        else:
            result = first_line <= second_line

        if args.reverse and len(second_line):
            result = not result

        return result

    with open(args.input, 'r') as f:
        line = read_line(f, args.line_separator)
        next_line = read_line(f, args.line_separator)
        while is_condition_execute(line, next_line):
            line = next_line
            next_line = read_line(f, args.line_separator)

        if len(next_line):
            return False
        else:
            return True


def sort(args):
    with open(args.input, 'r') as unsorted_file:
        buffer_size = args.buffer_size - (args.buffer_size
                                          % len(unsorted_file.readline()))
        unsorted_file.seek(0)
        temp_files = get_sorted_temp_files(args, unsorted_file, buffer_size)

        result_tempfile = merge(args, temp_files)

        with open(args.output, 'w') as result_file:
            line = read_line(result_tempfile, '\n')
            while len(line):
                result_file.write(line)
                line = read_line(result_tempfile, '\n')


def parse_args():
    parser = argparse.ArgumentParser()

    parser.add_argument('input', type=str,
                        help='Unsorted file')
    parser.add_argument('-o', '--output', type=str, default='sorted',
                        help='Output file. Default: \'sorted\n')

    parser.add_argument('-k', '--key', nargs='+', type=int,
                        help='Symbols for sorting.', required=False)

    parser.add_argument('-s', '--line_separator', default='\n',
                        help='Line separator. Default: \\n')
    parser.add_argument('--section_length', type=int, default='2',
                        help='Length of each section in line')
    parser.add_argument('-b', '--buffer_size', type=int, default=10000,
                        help='''Buffer size. It cant be over size of file and
                        very small''')
    parser.add_argument('-n', '--numeric', action='store_true',
                        help='To interpret parts of a line as numbers.')

    parser.add_argument('-r', '--reverse', action='store_true',
                        help='Reserve sorting')
    parser.add_argument('-c', '--check', action='store_true',
                        help='Check file for sorting according the rules.')

    return parser.parse_args()


def main():
    args = parse_args()

    if args.check:
        print 'File sorted: {}'.format(check(args))
    else:
        sort(args)


if __name__ == '__main__':
    main()
