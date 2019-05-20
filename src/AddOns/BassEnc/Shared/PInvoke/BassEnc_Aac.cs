using System;
using System.Runtime.InteropServices;

namespace ManagedBass.Enc
{
    /// <summary>
    /// BassEnc_AAC is an extension to the BassAAC add-on that allows BASS channels to be AAC encoded.
    /// </summary>
    public static class BassEnc_Aac
    {
#if __IOS__
        const string DllName = "__Internal";
#else
        const string DllName = "bassenc_aac";
#endif
                
        [DllImport(DllName)]
        static extern int BASS_Encode_AAC_GetVersion();

        /// <summary>
        /// Gets the Version of BassEnc_AAC that is loaded.
        /// </summary>
        public static Version Version => Extensions.GetVersion(BASS_Encode_AAC_GetVersion());

        [DllImport(DllName, CharSet = CharSet.Unicode)]
        static extern int BASS_Encode_AAC_Start(int Handle, string Options, EncodeFlags Flags, EncodeProcedureEx ProcedureEx, IntPtr User);

        /// <summary>
        /// Start Aac Encoding to <see cref="EncodeProcedureEx"/>.
        /// For best documentation on functionality see http://www.un4seen.com/doc/
        /// </summary>
        /// <param name="Handle">The channel handle... a HSTREAM, HMUSIC, or HRECORD.</param>
        /// <param name="Options">
        /// Encoder options... NULL = use defaults. 
        /// -t {type} ... "type" can be: 2/5/23/29/39. (The default is type 2 CBR.)
        /// See here for descriptions: https://en.wikipedia.org/wiki/MPEG-4_Part_3#MPEG-4_Audio_Object_Types
        /// v {vbr} ... "vbr" can be 0 (CBR) or 1-5 (VBR levels).
        /// -b {bitrate}...bitrate for CBR.
        /// Uses FDK-AAC Encoder Library: https://github.com/mstorsjo/fdk-aac
        /// </param>
        /// <param name="Flags">A combination of <see cref="EncodeFlags"/>
        /// EncodeFlags.Queue	Queue data to feed the encoder asynchronously. This prevents the data source (DSP system or BASS_Encode_Write call) getting blocked by the encoder, but if data is queud more quickly than the encoder can process it, that could result in lost data.
        /// EncodeFlags.Limit Limit the encoding rate to real-time speed, by introducing a delay when the rate is too high.With BASS 2.4.6 or above, this flag is ignored when the encoder is fed in a playback buffer update cycle (including BASS_Update and BASS_ChannelUpdate calls), to avoid possibly causing playback buffer underruns.Except for in those instances, this flag is applied automatically when the encoder is feeding a Shoutcast or Icecast server.
        /// EncodeFlags.UnlimitedCastDataRate    Don't limit the encoding rate to real-time speed when feeding a Shoutcast or Icecast server. This flag overrides the BASS_ENCODE_LIMIT flag.
        /// EncodeFlags.Pause Start the encoder in a paused state.
        /// EncodeFlags.Autofree Automatically free the encoder when the source channel is freed.If queuing is enabled, any remaining queued data will be sent to the encoder before it is freed.
        /// EncodeFlags.Unicode options is in UTF-16 form.Otherwise it should be UTF-8 or ISO-8859-1 (or a mix of the two).</param>
        /// <param name="ProcedureEx">Optional callback function to receive the encoded data... null = no callback.</param>

        /// <param name="User">User instance data to pass to the callback function.</param>
        /// <returns>The encoder handle is returned if the encoder is successfully started, else 0 is returned. Use <see cref="Bass.LastError"/> to get the error code</returns>
        /// <remarks>
        /// <see cref="BassEnc.EncodeStart(int,string,EncodeFlags,EncoderProcedure,IntPtr)"/> is used internally to apply the encoder to the source channel, so the remarks in its documentation also apply to this function. 
        /// </remarks>
        /// <exception cref="Errors.Handle"><paramref name="Handle"/> is not valid</exception>
        /// <exception cref="Errors.SampleFormat">The channel's sample format is not supported by the encoder.</exception>
        /// <exception cref="Errors.NotAvailable">This function is not available on platforms/architectures without an FPU.</exception>
        /// <exception cref="Errors.Unknown">Some other mystery problem! </exception>

        public static int Start(int Handle, string Options, EncodeFlags Flags, EncodeProcedureEx ProcedureEx, IntPtr User)
        {
            return BASS_Encode_AAC_Start(Handle, Options, Flags | EncodeFlags.Unicode, ProcedureEx, User);
        }

        [DllImport(DllName, CharSet = CharSet.Unicode)]
        static extern int BASS_Encode_AAC_StartFile(int Handle, string Options, EncodeFlags Flags, string FileName);

        /// <summary>
        /// Start Aac Encoding to File. For best documentation on functionality see http://www.un4seen.com/doc/
        /// </summary>
        /// <param name="Handle">The channel handle... a HSTREAM, HMUSIC, or HRECORD.</param>
        /// <param name="Options">
        /// Encoder options... NULL = use defaults. 
        /// -t {type} ... "type" can be: 2/5/23/29/39. (The default is type 2 CBR.)
        /// See here for descriptions: https://en.wikipedia.org/wiki/MPEG-4_Part_3#MPEG-4_Audio_Object_Types
        /// v {vbr} ... "vbr" can be 0 (CBR) or 1-5 (VBR levels).
        /// -b {bitrate}...bitrate for CBR.
        /// Uses FDK-AAC Encoder Library: https://github.com/mstorsjo/fdk-aac
        /// </param>
        /// <param name="Flags">A combination of <see cref="EncodeFlags"/>
        /// EncodeFlags.Queue	Queue data to feed the encoder asynchronously. This prevents the data source (DSP system or BASS_Encode_Write call) getting blocked by the encoder, but if data is queud more quickly than the encoder can process it, that could result in lost data.
        /// EncodeFlags.Limit Limit the encoding rate to real-time speed, by introducing a delay when the rate is too high.With BASS 2.4.6 or above, this flag is ignored when the encoder is fed in a playback buffer update cycle (including BASS_Update and BASS_ChannelUpdate calls), to avoid possibly causing playback buffer underruns.Except for in those instances, this flag is applied automatically when the encoder is feeding a Shoutcast or Icecast server.
        /// EncodeFlags.UnlimitedCastDataRate    Don't limit the encoding rate to real-time speed when feeding a Shoutcast or Icecast server. This flag overrides the BASS_ENCODE_LIMIT flag.
        /// EncodeFlags.Pause Start the encoder in a paused state.
        /// EncodeFlags.Autofree Automatically free the encoder when the source channel is freed.If queuing is enabled, any remaining queued data will be sent to the encoder before it is freed.
        /// EncodeFlags.Unicode options is in UTF-16 form.Otherwise it should be UTF-8 or ISO-8859-1 (or a mix of the two).</param>
        /// <param name="FileName">Output filename... null = no output file.</param>
        /// <returns>The encoder handle is returned if the encoder is successfully started, else 0 is returned. Use <see cref="Bass.LastError"/> to get the error code</returns>
        /// <remarks>
        /// <see cref="BassEnc.EncodeStart(int,string,EncodeFlags,EncoderProcedure,IntPtr)"/> is used internally to apply the encoder to the source channel, so the remarks in its documentation also apply to this function. 
        /// 
        /// </remarks>
        /// <exception cref="Errors.Handle"><paramref name="Handle"/> is not valid</exception>
        /// <exception cref="Errors.SampleFormat">The channel's sample format is not supported by the encoder.</exception>
        /// <exception cref="Errors.Create">The file could not be created.</exception>
        /// <exception cref="Errors.NotAvailable">This function is not available on platforms/architectures without an FPU.</exception>
        /// <exception cref="Errors.Unknown">Some other mystery problem! </exception>
        public static int Start(int Handle, string Options, EncodeFlags Flags, string FileName)
        {
            return BASS_Encode_AAC_StartFile(Handle, Options, Flags | EncodeFlags.Unicode, FileName);
        }
    }
}