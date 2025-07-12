using Microsoft.Extensions.DependencyInjection;
using Core.Services;
using Core.Services.Interfaces;

var services = new ServiceCollection();

services.AddScoped<ITorrentFileService, TorrentFileService>();
services.AddScoped<IBEncoderService, BEncoderService>();

var serviceProvider = services.BuildServiceProvider();
var torrentFileService = serviceProvider.GetService<ITorrentFileService>();

torrentFileService!.GetInfoHash("PATH_TO_TORRENT_FILE");
