using System;
using UnityEngine;
using Unity.Netcode;
using System.Collections.Generic;

public class NetworkCommandLine : MonoBehaviour
{
    private NetworkManager _networkManager;

    void Start()
    {
        _networkManager = GetComponentInParent<NetworkManager>();

        if (Application.isEditor) return;

        var args = GetCommandlineArgs();

        if (!args.TryGetValue("-mode", out var mode)) return;
        switch (mode)
        {
            case "server":
                _networkManager.StartServer();
                break;
            case "client":
                _networkManager.StartClient();
                break;
            case "host":
                _networkManager.StartHost();
                break;
            default:
                Debug.LogError($"Unknown mode: {mode}");
                break;
        }
    }

    private Dictionary<string, string> GetCommandlineArgs()
    {
        var argDictionary = new Dictionary<string, string>();

        var args = Environment.GetCommandLineArgs();

        for (var i = 0; i < args.Length; i++)
        {
            var arg = args[i].ToLower();
            if (!arg.StartsWith("-")) continue;
            var value = i < args.Length - 1 ? args[i + 1].ToLower() : null;
            value = (value?.StartsWith("-") ?? false) ? null : value;
            argDictionary.Add(arg, value);
        }

        return argDictionary;
    }
}