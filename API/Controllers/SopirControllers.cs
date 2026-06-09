//using HYMAPSOPIR.Library;
using HYMAPSOPIR;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using System;
using System.Linq;

namespace HYMAP.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SopirController : ControllerBase
    {
        // GET: api/Sopir/{username}/tanggal 2026-04-22
        [HttpGet("{username}/tugas")]
        public IActionResult GetTugas(string username, [FromQuery] DateTime tanggal)
        {
            var sopir = DatabaseSimulasi.SopirDB.FirstOrDefault(s => s.Username == username);
            if (sopir == null) return NotFound("Sopir tidak ditemukan.");

            sopir.SetTugasBerdasarkanArmada(DatabaseSimulasi.PelangganDB, tanggal);

            return Ok(sopir.DaftarTugasHariIni);
        }

        // POST: api/Sopir/{username}/tugas
        [HttpPost("{username}/tugas")]
        public IActionResult TambahTugasManual(string username, [FromBody] TambahTugas post)
        {
            var sopir = DatabaseSimulasi.SopirDB.FirstOrDefault(s => s.Username == username);
            var pelanggan = DatabaseSimulasi.PelangganDB.FirstOrDefault(p => p.IdPelanggan == post.IdPelanggan);

            if (sopir == null || pelanggan == null) return BadRequest("Data tidak valid.");

            var tugasBaru = new Pengiriman(pelanggan, post.TanggalTugas);
            sopir.DaftarTugasHariIni.Add(tugasBaru);

            return CreatedAtAction(nameof(GetTugas), new { username = username }, tugasBaru);
        }

        // PUT: api/Sopir/{username}/tugas/{idPelanggan}
        [HttpPut("{username}/tugas/{idPelanggan}")]
        public IActionResult UpdateStatus(string username, string idPelanggan, [FromBody] UpdatePengiriman put)
        {
            var sopir = DatabaseSimulasi.SopirDB.FirstOrDefault(s => s.Username == username);
            if (sopir == null) return NotFound();

            var tugas = sopir.DaftarTugasHariIni.FirstOrDefault(t => t.DataPelanggan.IdPelanggan == idPelanggan);
            if (tugas == null) return NotFound("Tugas pengiriman tidak ditemukan di daftar sopir.");

            sopir.EksekusiPengiriman(tugas, put.StatusKirim, put.StatusBayar, put.BuktiFoto);

            return Ok(new { Pesan = "Update Berhasil", Data = tugas });
        }

        // DELETE: api/Sopir/{username}/tugas/{idPelanggan}
        [HttpDelete("{username}/tugas/{idPelanggan}")]
        public IActionResult HapusTugas(string username, string idPelanggan)
        {
            var sopir = DatabaseSimulasi.SopirDB.FirstOrDefault(s => s.Username == username);
            if (sopir == null) return NotFound();

            var tugas = sopir.DaftarTugasHariIni.FirstOrDefault(t => t.DataPelanggan.IdPelanggan == idPelanggan);
            if (tugas == null) return NotFound();

            sopir.DaftarTugasHariIni.Remove(tugas);
            return Ok(new { Pesan = $"Tugas untuk pelanggan {idPelanggan} telah dihapus dari daftar." });
        }
    }
﻿using Library.Database;
using HYMAPSOPIR;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HYMAP.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SopirController : ControllerBase
    {
        // GET: api/Sopir/{username}/tugas?tanggal=2026-06-09
        [HttpGet("{username}/tugas")]
        public IActionResult GetTugas(string username, [FromQuery] DateTime tanggal)
        {
            // Ambil seluruh data sopir dari database MySQL
            List<Sopir> dataSopirDb = SopirDAO.GetAllSopir();
            var sopir = dataSopirDb.FirstOrDefault(s => s.Username == username);
            if (sopir == null) return NotFound("Sopir tidak ditemukan.");

            // Ambil seluruh data pelanggan dari database MySQL
            List<Pelanggan> dataPelangganDb = PelangganDAO.GetAllPelanggan();

            sopir.SetTugasBerdasarkanArmada(dataPelangganDb, tanggal);

            return Ok(sopir.DaftarTugasHariIni);
        }

        // POST: api/Sopir/{username}/tugas
        [HttpPost("{username}/tugas")]
        public IActionResult TambahTugasManual(string username, [FromBody] TambahTugas post)
        {
            List<Sopir> dataSopirDb = SopirDAO.GetAllSopir();
            var sopir = dataSopirDb.FirstOrDefault(s => s.Username == username);

            List<Pelanggan> dataPelangganDb = PelangganDAO.GetAllPelanggan();
            var pelanggan = dataPelangganDb.FirstOrDefault(p => p.IdPelanggan == post.IdPelanggan);

            if (sopir == null || pelanggan == null) return BadRequest("Data tidak valid.");

            // Buat objek pengiriman baru
            var tugasBaru = new Pengiriman(pelanggan, post.TanggalTugas);

            PengirimanDAO.SimpanJadwalPengiriman(tugasBaru);

            return CreatedAtAction(nameof(GetTugas), new { username = username }, tugasBaru);
        }

        // PUT: api/Sopir/{username}/tugas/{idPelanggan}
        [HttpPut("{username}/tugas/{idPelanggan}")]
        public IActionResult UpdateStatus(string username, string idPelanggan, [FromBody] UpdatePengiriman put)
        {
            List<Sopir> dataSopirDb = SopirDAO.GetAllSopir();
            var sopir = dataSopirDb.FirstOrDefault(s => s.Username == username);
            if (sopir == null) return NotFound();

            List<Pelanggan> dataPelangganDb = PelangganDAO.GetAllPelanggan();
            sopir.SetTugasBerdasarkanArmada(dataPelangganDb, DateTime.Now);

            var tugas = sopir.DaftarTugasHariIni.FirstOrDefault(t => t.DataPelanggan.IdPelanggan == idPelanggan);
            if (tugas == null) return NotFound("Tugas pengiriman tidak ditemukan di daftar sopir.");

            sopir.EksekusiPengiriman(tugas, put.StatusKirim, put.StatusBayar, put.BuktiFoto);

            PengirimanDAO.SimpanJadwalPengiriman(tugas);

            return Ok(new { Pesan = "Update Berhasil", Data = tugas });
        }

        // DELETE: api/Sopir/{username}/tugas/{idPelanggan}
        [HttpDelete("{username}/tugas/{idPelanggan}")]
        public IActionResult HapusTugas(string username, string idPelanggan)
        {
            List<Sopir> dataSopirDb = SopirDAO.GetAllSopir();
            var sopir = dataSopirDb.FirstOrDefault(s => s.Username == username);
            if (sopir == null) return NotFound();

            List<Pelanggan> dataPelangganDb = PelangganDAO.GetAllPelanggan();
            sopir.SetTugasBerdasarkanArmada(dataPelangganDb, DateTime.Now);

            var tugas = sopir.DaftarTugasHariIni.FirstOrDefault(t => t.DataPelanggan.IdPelanggan == idPelanggan);
            if (tugas == null) return NotFound();

            sopir.DaftarTugasHariIni.Remove(tugas);

            return Ok(new { Pesan = $"Tugas untuk pelanggan {idPelanggan} telah dihapus dari daftar." });
        }
    }
}