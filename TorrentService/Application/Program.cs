using Core.Interfaces;
using Core.Models;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;


var services = new ServiceCollection();

services.AddScoped<ITorrentFileService, TorrentFileService>();
services.AddScoped<IBEncoderService, BEncoderService>();

var serviceProvider = services.BuildServiceProvider();
var torrentFileService = serviceProvider.GetService<ITorrentFileService>();

TorrentMetadata metadata = torrentFileService!.Parse("C:\\Users\\karee\\Documents\\GitHub\\torrent_service\\TorrentService\\elephant.torrent");
Console.WriteLine(metadata);