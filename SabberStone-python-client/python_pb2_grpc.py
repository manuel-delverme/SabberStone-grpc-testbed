# Generated by the gRPC Python protocol compiler plugin. DO NOT EDIT!
import grpc

import python_pb2 as python__pb2


class SabberStonePythonStub(object):
  # missing associated documentation comment in .proto file
  pass

  def __init__(self, channel):
    """Constructor.

    Args:
      channel: A grpc.Channel.
    """
    self.NewGame = channel.unary_unary(
        '/SabberStonePython/NewGame',
        request_serializer=python__pb2.DeckStrings.SerializeToString,
        response_deserializer=python__pb2.Game.FromString,
        )
    self.GetOptions = channel.unary_unary(
        '/SabberStonePython/GetOptions',
        request_serializer=python__pb2.Game.SerializeToString,
        response_deserializer=python__pb2.Options.FromString,
        )
    self.Process = channel.unary_unary(
        '/SabberStonePython/Process',
        request_serializer=python__pb2.Option.SerializeToString,
        response_deserializer=python__pb2.Game.FromString,
        )
    self.GetCardDictionary = channel.unary_unary(
        '/SabberStonePython/GetCardDictionary',
        request_serializer=python__pb2.Empty.SerializeToString,
        response_deserializer=python__pb2.Cards.FromString,
        )


class SabberStonePythonServicer(object):
  # missing associated documentation comment in .proto file
  pass

  def NewGame(self, request, context):
    # missing associated documentation comment in .proto file
    pass
    context.set_code(grpc.StatusCode.UNIMPLEMENTED)
    context.set_details('Method not implemented!')
    raise NotImplementedError('Method not implemented!')

  def GetOptions(self, request, context):
    # missing associated documentation comment in .proto file
    pass
    context.set_code(grpc.StatusCode.UNIMPLEMENTED)
    context.set_details('Method not implemented!')
    raise NotImplementedError('Method not implemented!')

  def Process(self, request, context):
    # missing associated documentation comment in .proto file
    pass
    context.set_code(grpc.StatusCode.UNIMPLEMENTED)
    context.set_details('Method not implemented!')
    raise NotImplementedError('Method not implemented!')

  def GetCardDictionary(self, request, context):
    # missing associated documentation comment in .proto file
    pass
    context.set_code(grpc.StatusCode.UNIMPLEMENTED)
    context.set_details('Method not implemented!')
    raise NotImplementedError('Method not implemented!')


def add_SabberStonePythonServicer_to_server(servicer, server):
  rpc_method_handlers = {
      'NewGame': grpc.unary_unary_rpc_method_handler(
          servicer.NewGame,
          request_deserializer=python__pb2.DeckStrings.FromString,
          response_serializer=python__pb2.Game.SerializeToString,
      ),
      'GetOptions': grpc.unary_unary_rpc_method_handler(
          servicer.GetOptions,
          request_deserializer=python__pb2.Game.FromString,
          response_serializer=python__pb2.Options.SerializeToString,
      ),
      'Process': grpc.unary_unary_rpc_method_handler(
          servicer.Process,
          request_deserializer=python__pb2.Option.FromString,
          response_serializer=python__pb2.Game.SerializeToString,
      ),
      'GetCardDictionary': grpc.unary_unary_rpc_method_handler(
          servicer.GetCardDictionary,
          request_deserializer=python__pb2.Empty.FromString,
          response_serializer=python__pb2.Cards.SerializeToString,
      ),
  }
  generic_handler = grpc.method_handlers_generic_handler(
      'SabberStonePython', rpc_method_handlers)
  server.add_generic_rpc_handlers((generic_handler,))
