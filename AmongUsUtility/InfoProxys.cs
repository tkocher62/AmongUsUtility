using Hazel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmongUsUtility
{
	public class TaskInfoProxy
	{

		public GameData.CBOMPDNBEIF original;

		public TaskInfoProxy(GameData.CBOMPDNBEIF original)
		{
			this.original = original;
		}
	}

	public class PlayerInfoProxy
	{
		public GameData.IHEKEPMDGIJ original;

		public PlayerInfoProxy(GameData.IHEKEPMDGIJ obfuscatedClass)
		{
			original = obfuscatedClass;
		}

		public void Serialize(MessageWriter writer)
		{
			original.FKGAJNCFAPL(writer);
		}

		public void Deserialize(MessageReader reader)
		{
			original.MNAABPPBENI(reader);
		}

		public TaskInfoProxy FindTaskById(uint taskId)
		{
			var result = original.CIPCBHAHMEA(taskId);
			return new TaskInfoProxy(result);
		}

		public byte PlayerId => original.FIOIBHIDDOC;

		public string PlayerName
		{
			get => original.EKHEPECKPKK;
			set => original.EKHEPECKPKK = value;
		}

		public byte ColorId
		{
			get => original.LHKAPPDILFP;
			set => original.LHKAPPDILFP = value;
		}
		public uint HatId
		{
			get => original.LJNAHAIMDOC;
			set => original.LJNAHAIMDOC = value;
		}
		public uint PetId
		{
			get => original.IKJHHHFMAIJ;
			set => original.IKJHHHFMAIJ = value;
		}
		public uint SkinId
		{
			get => original.PJBKAJGBPAD;
			set => original.PJBKAJGBPAD = value;
		}
		public bool Disconnected
		{
			get => original.MHAHKDGBNDM;
			set => original.MHAHKDGBNDM = value;
		}

		public List<TaskInfoProxy> Tasks
		{
			get
			{
				List<TaskInfoProxy> o = new List<TaskInfoProxy>();
				foreach (var orig in original.IHACFCJPFCF)
				{
					o.Add(new TaskInfoProxy(orig));
				}
				return o;
			}
			set
			{
				Il2CppSystem.Collections.Generic.List<GameData.CBOMPDNBEIF> set = new Il2CppSystem.Collections.Generic.List<GameData.CBOMPDNBEIF>();

				foreach (var task in value)
				{
					set.Add(task.original);
				}

				original.IHACFCJPFCF = set;
			}
		}

		public bool IsImpostor
		{
			get => original.LODLBBJNGKB;
			set => original.LODLBBJNGKB = value;
		}
		public bool IsDead
		{
			get => original.DMFDFKEJHLH;
			set => original.DMFDFKEJHLH = value;
		}
		public PlayerControl _object
		{
			get => original.OOGBDGNGIEM;
			set => original.OOGBDGNGIEM = value;
		}

		public PlayerControl Object => _object;

	}
}
