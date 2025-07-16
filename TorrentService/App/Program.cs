using Core.Models;
using Microsoft.Extensions.DependencyInjection;
using Core.Services;
using Core.Services.Interfaces;


var services = new ServiceCollection();

services.AddScoped<ITorrentFileService, TorrentFileService>();
services.AddScoped<IBEncoderService, BEncoderService>();

var serviceProvider = services.BuildServiceProvider();
var torrentFileService = serviceProvider.GetService<ITorrentFileService>();

TorrentMetadata metadata = torrentFileService!.Parse("PATH_TO_TORRENT_FILE");
Console.WriteLine(metadata);