using AutoMapper;
using Automapper_JWTTokens_Demo.Data;
using Automapper_JWTTokens_Demo.Dtos;
using Automapper_JWTTokens_Demo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Automapper_JWTTokens_Demo.Services
{

    public class CharacterService : ICharacterService
    {
        
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public CharacterService(IMapper mapper, DataContext context)
        {
            this._mapper = mapper;
            this._context = context;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            //characters.Add(newCharacter);
            //serviceResponse.Data = characters;
            Character character = _mapper.Map<Character>(newCharacter);
            character.Id = _context.Characters.Max(a => a.Id) + 1;
            _context.Characters.Add(character);
            await _context.SaveChangesAsync();
            serviceResponse.Data = _mapper.Map<List<GetCharacterDto>>(_context.Characters.ToList());
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            try
            {
                Character character = _context.Characters.First(a => a.Id == id);
                _context.Remove(character);
                await _context.SaveChangesAsync();
                //characters.Remove(character);
                serviceResponse.Data = _mapper.Map<List<GetCharacterDto>>(_context.Characters.ToList());
            }
            catch(Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Character not found"+"OR"+ex.StackTrace; 
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            List<Character> dbcharacters = await _context.Characters.ToListAsync();
            //serviceResponse.Data = characters;
            serviceResponse.Data = _mapper.Map<List<GetCharacterDto>>(dbcharacters);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            ServiceResponse<GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto>();
            //serviceResponse.Data= characters.FirstOrDefault(a => a.Id == id);
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(await _context.Characters.FirstOrDefaultAsync(a => a.Id == id));
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
        {
            ServiceResponse<GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto>();
            try
            {
                Character character = _context.Characters.FirstOrDefault(a => a.Id == updatedCharacter.Id);
                character.Name = updatedCharacter.Name;
                character.Class = updatedCharacter.Class;
                character.Defense = updatedCharacter.Defense;
                character.HitPoints = updatedCharacter.HitPoints;
                character.Intelligence = updatedCharacter.Intelligence;
                character.Strength = updatedCharacter.Strength;
                await _context.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;


        }
    }
}
