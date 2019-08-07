# functionc call protocol
from struct import *


def call_function(socket, mmf, function_id, *args):
    # Send function id as a byte
    socket.send(bytes([function_id]))
    for arg in args:
        if type(arg) is int:
            socket.send(pack('i', arg))
    return _retrieve_returned_value(socket, mmf)


def call_function(socket, mmf, function_id, int_arg: int):
    socket.send(bytes([function_id]))
    # Send 1 int argument.
    socket.send(b'i')
    socket.send(pack('i', int_arg))
    socket.send(b'4')
    return _retrieve_returned_value(socket, mmf)


def _retrieve_returned_value(socket, mmf):
    size = unpack('I', socket.recv(4))[0]
    print('function returns a structure of size {0}'.format(size))
    print(mmf[0:size])
    return mmf[0:size].tobytes()


def _encode_argument(arg):
    pass
